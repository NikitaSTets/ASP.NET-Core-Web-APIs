﻿namespace ASP.NET_Core_Web_APIs.Services.Interfaces
{
    public interface IMakeNameValidator
    {
        public OperationResult<bool> Validate(string makeName);
    }
}