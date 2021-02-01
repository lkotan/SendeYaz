﻿namespace SendeYaz.Core.Messages
{
    public static class AccountMessage
    {
        public static string AccountNotFound => "Kullanıcı Bulunamadı.";
        public static string AccountIsBlocked => "Kullanıcı Pasif Edilmiş.";
        public static string AccountIsExists => "Kullanıcı Kayıtlı.";
        public static string PasswordWrong => "Hatalı Şifre";
        public static string LoginSuccessful => "Giriş yapıldı.";
        public static string LoginSuccessfulByRefreshToken => "Giriş yapıldı.";
        public static string LogoutSuccessful => "Çıkış yapıldı.";
        public static string AuthenticationError => "Kullanıcı Girişi Yapın";
        public static string TokenNotFound => "Token Mevcut değil.";
        public static string TokenIsUsed => "Token zaten kullanılmış.";
        public static string TokenExpired => "Token zaman aşımına uğramış.";
        public static string PasswordChangeSuccessful => "Şifre başarıyla değiştirildi";
        public static string PasswordIsNull => "Şifre null veya boş olamaz";
    }
}
