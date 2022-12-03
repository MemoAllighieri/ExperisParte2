using System;

namespace Parte2.DTOs
{
    public class ResponseDTO
    {
        public ResponseDTO()
        {
            this.Status = 0;            
            Data = new Object();
        }        
        public int Status { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
    }
}
