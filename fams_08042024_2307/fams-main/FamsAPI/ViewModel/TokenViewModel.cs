﻿namespace FamsAPI.ViewModel
{
    public class TokenViewModel
    {
        public string AccessTokenToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiredAt {  get; set; }  
    }
}
