using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Client.DTOs
{
    public class ApiResponseDTO<T>
    {
        public int Code { get; set; }

        public T Value { get; set; }
    }
}
