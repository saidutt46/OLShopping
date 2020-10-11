using System;
using System.Collections.Generic;

namespace DCommerce.Dto.Shared
{
    public class BaseDtoListResponse<T>
    {
        public IList<T> Payload { get; set; }
        public DateTime MessageDateTime { get; set; }
        public string Error { get; set; }

        public BaseDtoListResponse(IList<T> payload)
        {
            Payload = payload;
            MessageDateTime = DateTime.UtcNow;
        }

        public BaseDtoListResponse(string error)
        {
            MessageDateTime = DateTime.UtcNow;
            Error = error;
        }
    }
}
