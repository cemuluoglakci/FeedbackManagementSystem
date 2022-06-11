using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using CoreFMS.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LinqStatistics;
using System.IO;

namespace ApplicationFMS.Handlers.Report.CustomerStatistics
{
    public class CustomerStatisticsQueryHandler : IRequestHandler<CustomerStatisticsQuery, BaseResponse>
    {
        private readonly IFMSDataContext _context;
        private readonly ICurrentUser? _currentUser;

        public CustomerStatisticsQueryHandler(IFMSDataContext context, ICurrentUser? currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse> Handle(CustomerStatisticsQuery request, CancellationToken cancellationToken)
        {
            if (_currentUser == null)
            {
                return BaseResponse.Fail("User Identity could not defined.");
            }

            if (_currentUser.UserDetail.RoleName != Constants.CompanyManagerRole)
            {
                return BaseResponse.Fail("User role is not authorized.");
            }

            IQueryable<Feedback>? feedbackQuery = _context.Feedback
                .Where(x => x.CompanyId == _currentUser.UserDetail.CompanyId && x.IsActive);

            // Filter accourding to every query
            if (request.ProductId > 0)
            {
                feedbackQuery = feedbackQuery.Where(x => x.ProductId == request.ProductId);
            }
            if (request.TypeId > 0)
            {
                feedbackQuery = feedbackQuery.Where(x => x.TypeId == request.TypeId);
            }

            var interactedUserIdQuery = feedbackQuery.Select(x => x.UserId).Distinct();

            var interactedUsersQuery = _context.User.Where(x => interactedUserIdQuery.Contains(x.Id));

            var viewModel = new CustomerStatisticsVm
            {
                TotalFeedbackCount = await feedbackQuery.CountAsync(cancellationToken),
                UserCountPostedFeedback = await interactedUserIdQuery.CountAsync(cancellationToken)
            };

            var userAgeList = interactedUsersQuery
                .Where(x => x.BirthDate.HasValue)
                .Select(x => (DateTime.Now - x.BirthDate).Value.Days / 365)
                .AsEnumerable();

            var accountAgeList = interactedUsersQuery
                .Where(x => x.RegisteredAt.HasValue)
                .Select(x => (DateTime.Now - x.RegisteredAt).Value.Days)
                .AsEnumerable();

            var cityStatistics = interactedUsersQuery
                .Where(x => x.CityId.HasValue)
                .GroupBy(x => new { x.CityId, x.City.CityName })
                .Select(x => new StatisticalSubList
                {
                    Id = (int)x.Key.CityId,
                    Name = x.Key.CityName,
                    Count = x.Count()
                }).ToList();

            var educationStatistics = interactedUsersQuery
                .Where(x => x.EducationId.HasValue)
                .GroupBy(x => new { x.EducationId, x.Education.EducationName})
                .Select(x => new StatisticalSubList
                {
                    Id = (int)x.Key.EducationId,
                    Name = x.Key.EducationName,
                    Count = x.Count()
                }).ToList();

            var customerStatistics = new UserStatistics
            {
                MeanUserAge = userAgeList.Average(),
                UserAgeInterval = Bucketize(userAgeList, 10),
                MeanAccountAge = accountAgeList.Average(),
                AccountAgeInterval = Bucketize(accountAgeList, 10),
                CityDistribution = cityStatistics,
                EducationDistribution = educationStatistics,
            };

            viewModel.CustomerStatistics = customerStatistics;

            return new BaseResponse(viewModel);
        }



        public static List<StatisticalSubList> Bucketize(IEnumerable<int> source, int totalBuckets)
        {
            var min = source.Min();
            var max = source.Max();
            var bucketSize = (max - min) / totalBuckets;

            var buckets = new List<StatisticalSubList>(totalBuckets);
            for (int i = 0; i < totalBuckets; i++)
            {
                string lowerLimit = (min + i * bucketSize).ToString();
                string upperLimit;
                if (i + 1 < totalBuckets)
                {
                    upperLimit = (min + (i + 1) * bucketSize).ToString();
                }
                else
                {
                    upperLimit = max.ToString();
                }

                buckets.Add(new StatisticalSubList
                {
                    Id = i,
                    Name = lowerLimit + " - " + upperLimit,
                    Count = 0
                });
            }

            foreach (var value in source)
            {
                int bucketIndex = 0;
                if (bucketSize > 0)
                {
                    bucketIndex = (int)((value - min) / bucketSize);
                    if (bucketIndex == totalBuckets)
                    {
                        bucketIndex--;
                    }
                }
                buckets[bucketIndex].Count++;
            }


            return buckets;
        }

    }
}
