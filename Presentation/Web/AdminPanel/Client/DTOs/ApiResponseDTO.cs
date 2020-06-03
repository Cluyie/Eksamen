using UCLDreamTeam.SharedInterfaces.Interfaces;
using AdminPanel.Client.Models;
using System;

namespace AdminPanel.Client.DTOs
{
    public class ApiResponseDTO<T> : IApiResponse<T>
    {
        public T Value { get; set; }
        public ApiResponseCode Code { get; set; }
    }
}