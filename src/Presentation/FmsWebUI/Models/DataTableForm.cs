using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace FmsWebUI.Models
{
    public class DataTableForm
    {
        public string Draw;
        public string Start;
        public string SortColumn;
        public string IsCheckedString;
        public string IsActiveString;
        public string Length;
        public string SortColumnDirection;
        public string SearchValue;
        public int PageSize;
        public int Skip;
        public int PageNumber = 1;

        public bool IsAscending;
        public bool? IsChecked;
        public bool? IsActive;

        public DataTableForm(IFormCollection form)
        {
            Draw = form["draw"].FirstOrDefault();
            Start = form["start"].FirstOrDefault();
            SortColumn = form["columns[" + form["order[0][column]"].FirstOrDefault() + "][data]"].FirstOrDefault();
            IsActiveString = form["columns[1][search][value]"].FirstOrDefault();
            IsCheckedString = form["columns[2][search][value]"].FirstOrDefault();
            Length = form["length"].FirstOrDefault();
            SortColumnDirection = form["order[0][dir]"].FirstOrDefault();
            SearchValue = form["search[value]"].FirstOrDefault();
            PageSize = Length != null ? Convert.ToInt32(Length) : 0;
            Skip = Start != null ? Convert.ToInt32(Start) : 0;

            if (Skip>0) PageNumber = (Skip/PageSize)+1;
            IsAscending = SortColumnDirection == "asc" ? true : false;
            IsChecked = Boolean.TryParse(IsCheckedString, out var tempCheckedResult) 
                ? Boolean.Parse(IsCheckedString) : null;
            IsActive = Boolean.TryParse(IsActiveString, out var tempActiveResult) 
                ? Boolean.Parse(IsActiveString) : null;
        }
 


    }
}
