﻿namespace Ambev.DeveloperEvaluation.WebApi.Common
{
    public class ListRequest
    {
        public string? _Order { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
