﻿using System.Text;
using ApplicationFMS.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using AutoMapper;

namespace FmsWebUI.Controllers
{
    public abstract class BaseController : Controller
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected async Task<BaseResponse> Mediate(IRequest<BaseResponse> request)
        {
            var response = await Mediator.Send<BaseResponse>(request);
            CreateRuntimeMessage(response);
            return response;
        }

        private void CreateRuntimeMessage(BaseResponse response)
        {
            if (!response.Meta.SuccessStatus)
            {
                TempData["Message"] = response.Meta.Message;
                if (response.data != null)
                {
                    StringBuilder validationMessageBuilder = new StringBuilder();
                    var validationDictionary = ToDictionary<string[]>(response.data);
                    validationMessageBuilder.Append("<ul>");
                    foreach (var item in validationDictionary)
                    {
                        validationMessageBuilder.Append("<li>" + item.Key + " </li> <ul>"  );

                        foreach (var exception in item.Value)
                        {
                            validationMessageBuilder.Append("<li>" + exception+ " </li>"  );
                        }
                        validationMessageBuilder.Append("</ul>");
                    }
                    validationMessageBuilder.Append("</ul>");
                    TempData["ValidationMessage"] = validationMessageBuilder.ToString();
                }
            }
            
        }

        private Dictionary<string, TValue> ToDictionary<TValue>(object obj)
        {       
            var json = JsonConvert.SerializeObject(obj);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, TValue>>(json);   
            return dictionary;
        }

    }
}
