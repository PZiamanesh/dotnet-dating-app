﻿using CloudinaryDotNet;

namespace API.Helpers
{
    public class UserParams
    {
        private const int maxPageSize = 50;

        private int pageSize = 10;

        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => pageSize;
            set
            {
                if (value <= 0)
                {
                    pageSize = 10;
                }
                else if (value > maxPageSize)
                {
                    pageSize = maxPageSize;
                }
                else
                {
                    pageSize = value;
                }
            }
        }

        public string? Gender { get; set; }

        public string? CurrentUserName { get; set; }
    }
}
