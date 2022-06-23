using ApplicationFMS.Handlers.Comments.Commands.ToggleActive;
using ApplicationFMS.Handlers.Comments.Commands.ToggleChecked;
using ApplicationFMS.Handlers.Comments.Queries.GetCommentList;
using ApplicationFMS.Handlers.Feedbacks.Commands.ToggleActive;
using ApplicationFMS.Handlers.Feedbacks.Commands.ToggleChecked;
using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackList;
using ApplicationFMS.Handlers.Replies.Commands.ToggleActive;
using ApplicationFMS.Handlers.Replies.Commands.ToggleChecked;
using ApplicationFMS.Handlers.Replies.Queries.GetReplyList;
using ApplicationFMS.Handlers.System.Commands.SetOperationalMode;
using ApplicationFMS.Handlers.System.Commands.SetTimeoutDuration;
using ApplicationFMS.Handlers.System.Queries.GetLogs;
using ApplicationFMS.Handlers.System.Queries.GetOperationalMode;
using ApplicationFMS.Handlers.System.Queries.GetTimeoutDuration;
using ApplicationFMS.Handlers.UserHandlers.Commands.ToggleUserAbility;
using ApplicationFMS.Handlers.UserHandlers.Queries.GetUserList;
using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using FmsWebUI.Helper;
using FmsWebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace FmsWebUI.Controllers
{
    [Authorize(Constants.AdminRole)]

    public class AdminController : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> System()
        {
            var operationalModeId = ((BaseLookup)(await Mediate(new GetOperationalModeQuery())).data).Id;
            var timeOutDuration = (int)(await Mediate(new GetTimeoutDurationQuery())).data;

            return View(new SystemSettings
            {
                ModeOfOperation = operationalModeId,
                TimeOutDuration = timeOutDuration
            });
        }

        [HttpPost]
        public async Task<IActionResult> System(SystemSettings systemSettings)
        {
            await Mediate(new SetOperationalModeCommand { ModeId = systemSettings.ModeOfOperation });
            await Mediate(new SetTimeoutDurationCommand { Value = systemSettings.TimeOutDuration });
            return View();
        }

        [HttpGet]
        public IActionResult UserList()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetUserList()
        {
            var dataTableForm = new DataTableForm(Request.Form);

            var searchQuery = CreateSearchQuery<GetUserListQuery>(dataTableForm);
            searchQuery.EmailQuery = dataTableForm.SearchValue;

            var response = await Mediate(searchQuery);
            var userListVm = (UserListVm)response.data;

            var jsonData = new { draw = dataTableForm.Draw, recordsFiltered = userListVm?.FilteredCount, recordsTotal = userListVm?.TotalCount, data = userListVm?.UserList };
            return Ok(jsonData);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteUser(int? id)
        {
            if (id is 0 or null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Result = "Error" });
            }
            var response = await Mediate(new ToggleUserAbilityCommand { Id = (int)id });
            return Json(new { Result = "OK" });
        }



        [HttpGet]
        public IActionResult FeedbackList()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetFeedbackList()
        {
            var dataTableForm = new DataTableForm(Request.Form);

            var searchQuery = CreatePostSearchQuery<GetFeedbackListQuery>(dataTableForm);
            searchQuery.TitleQuery = dataTableForm.SearchValue;

            var response = await Mediate(searchQuery);
            var feedbackListVm = (FeedbackListVm)response.data;

            var jsonData = new { draw = dataTableForm.Draw, recordsFiltered = feedbackListVm?.FilteredCount, recordsTotal = feedbackListVm?.TotalCount, data = feedbackListVm?.AdminFeedbackList };
            return Ok(jsonData);
        }

        [HttpPost]
        public async Task<JsonResult> CheckFeedback(int? id)
        {
            if (id is 0 or null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Result = "Error" });
            }
            var response = await Mediate(new ToggleCheckedFeedbackCommand { Id = (int)id });
            return Json(new { Result = "OK" });
        }

        [HttpPost]
        public async Task<JsonResult> DeleteFeedback(int? id)
        {
            if (id is 0 or null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Result = "Error" });
            }
            var response = await Mediate(new ToggleActiveFeedbackCommand { Id = (int)id });
            return Json(new { Result = "OK" });
        }




        [HttpGet]
        public IActionResult CommentList()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetCommentList()
        {
            var dataTableForm = new DataTableForm(Request.Form);

            var searchQuery = CreatePostSearchQuery<GetCommentListQuery>(dataTableForm);
            searchQuery.TextQuery = dataTableForm.SearchValue;

            var response = await Mediate(searchQuery);
            var commentListVm = (GetCommentListVm)response.data;

            var jsonData = new { draw = dataTableForm.Draw, recordsFiltered = commentListVm?.FilteredCount, recordsTotal = commentListVm?.TotalCount, data = commentListVm?.CommentList };
            return Ok(jsonData);
        }

        [HttpPost]
        public async Task<JsonResult> CheckComment(int? id)
        {
            if (id is 0 or null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Result = "Error" });
            }
            var response = await Mediate(new ToggleCheckedCommentCommand { Id = (int)id });
            return Json(new { Result = "OK" });
        }

        [HttpPost]
        public async Task<JsonResult> DeleteComment(int? id)
        {
            if (id is 0 or null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Result = "Error" });
            }
            var response = await Mediate(new ToggleActiveCommentCommand { Id = (int)id });
            return Json(new { Result = "OK" });
        }




        [HttpGet]
        public IActionResult ReplyList()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetReplyList()
        {
            var dataTableForm = new DataTableForm(Request.Form);

            var searchQuery = CreatePostSearchQuery<GetReplyListQuery>(dataTableForm);
            searchQuery.TextQuery = dataTableForm.SearchValue;

            var response = await Mediate(searchQuery);
            var replyListVm = (GetReplyListVm)response.data;

            var jsonData = new { draw = dataTableForm.Draw, recordsFiltered = replyListVm?.FilteredCount, recordsTotal = replyListVm?.TotalCount, data = replyListVm?.ReplyList };
            return Ok(jsonData);
        }

        [HttpPost]
        public async Task<JsonResult> CheckReply(int? id)
        {
            if (id is 0 or null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Result = "Error" });
            }
            var response = await Mediate(new ToggleCheckedReplyCommand { Id = (int)id });
            return Json(new { Result = "OK" });
        }

        [HttpPost]
        public async Task<JsonResult> DeleteReply(int? id)
        {
            if (id is 0 or null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Result = "Error" });
            }
            var response = await Mediate(new ToggleActiveReplyCommand { Id = (int)id });
            return Json(new { Result = "OK" });
        }



        [HttpGet]
        public IActionResult LogList()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetLogList()
        {
            var dataTableForm = new DataTableForm(Request.Form);

            var searchQuery = CreateSearchQuery<GetLogListQuery>(dataTableForm);
            searchQuery.Query = dataTableForm.SearchValue;

            var response = await Mediate(searchQuery);
            var logListVm = (LogListVm)response.data;

            var jsonData = new { draw = dataTableForm.Draw, recordsFiltered = logListVm?.FilteredCount, recordsTotal = logListVm?.TotalCount, data = logListVm?.LogList };
            return Ok(jsonData);
        }







        private T CreateSearchQuery<T>(DataTableForm dataTableForm) where T : class, ISearchQuery, new()
        {
            return new T
            {
                ObjectsPerPage = dataTableForm.PageSize,
                PageNumber = dataTableForm.PageNumber,
                SortColumn = dataTableForm.SortColumn ?? string.Empty,
                IsAscending = dataTableForm.IsAscending,
                IsActive = dataTableForm.IsActive
            };
        }

        private T CreatePostSearchQuery<T>(DataTableForm dataTableForm) where T : class, IPostSearchQuery, new()
        {
            var searchQuery = CreateSearchQuery<T>(dataTableForm);
            searchQuery.IsChecked = dataTableForm.IsChecked;
            return searchQuery;
        }

    }
}
