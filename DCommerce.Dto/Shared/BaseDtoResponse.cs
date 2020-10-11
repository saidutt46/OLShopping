using System;
using System.Collections.Generic;

namespace DCommerce.Dto.Shared
{
    public class BaseDtoResponse<T>
    {
        public T Payload { get; private set; }
        public DateTime MessageDateTime { get; set; }
        public string[] Error { get; private set; }

        public BaseDtoResponse(T payload)
        {
            Payload = payload;
            MessageDateTime = DateTime.UtcNow;
        }

        public BaseDtoResponse(string error)
        {
            MessageDateTime = DateTime.UtcNow;
            Error = Error;
        }
    }
}
