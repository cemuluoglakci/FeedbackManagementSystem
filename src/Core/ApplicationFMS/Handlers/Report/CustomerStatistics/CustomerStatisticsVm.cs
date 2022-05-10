using ApplicationFMS.Models;
using System.Collections.Generic;

namespace ApplicationFMS.Handlers.Report.CustomerStatistics
{
    public class CustomerStatisticsVm
    {
        public int TotalFeedbackCount { get; set; }
        public int UserCountPostedFeedback { get; set; }
        public UserStatistics? CustomerStatistics { get; set; }

        //public int TotalCommentCount { get; set; }
        //public int UserCountPostedComment { get; set; }

        //public int TotalReactionCount { get; set; }
        //public int UserCountReacted { get; set; }
    }

    public class UserStatistics
    {
        public double MeanUserAge { get; set; }
        public List<StatisticalSubList>? UserAgeInterval { get; set; }
        public double MeanAccountAge { get; set; }
        public List<StatisticalSubList>? AccountAgeInterval { get; set; }
        public List<StatisticalSubList>? EducationDistribution { get; set; }
        public List<StatisticalSubList>? CityDistribution { get; set; }
    }
}
