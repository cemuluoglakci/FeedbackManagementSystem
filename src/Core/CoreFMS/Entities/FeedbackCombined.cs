using System;
using System.Collections.Generic;

namespace CoreFMS.Entities
{
    public partial class FeedbackCombined
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;
        public int? SectorId { get; set; }
        public string? SectorName { get; set; }
        public int? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public int TypeId { get; set; }
        public string? TypeName { get; set; }
        public int? SubTypeId { get; set; }
        public string? SubTypeName { get; set; }
        public int? Shared { get; set; }
        public int? LikeCount { get; set; }
        public int? DislikeCount { get; set; }
        public int IsAnonym { get; set; }
        public int IsActive { get; set; }
        public int IsChecked { get; set; }
        public int IsReplied { get; set; }
        public int IsSolved { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
