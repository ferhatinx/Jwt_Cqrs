﻿namespace Jwt_CqrsUI.Models;
public class JwtTokenResponseModel
{
    public string Token { get; set; }
    public DateTime ExpireDate { get; set; }
}
