namespace UniBazaarLite.Data
{
    // Kullanıcının adını, e-postasını ve rolünü bir arada tutan record type.
    public record UserDto(string Name, string Email, string Role);

    public static class SimulatedUserData
    {
        // Bu simüle edilmiş kullanıcı veritabanımızdır.
        private static readonly List<UserDto> _users = new()
        {
            // Sisteme önceden tanımlanmış test kullanıcıları ekleniyor.
            new UserDto("Test User 1", "testuser1@example.com", "User"),
            new UserDto("Test User 2", "testuser2@example.com", "User"),
            new UserDto("Test User 3", "testuser3@example.com", "User")
        };

        // Dışarıdan bir e-posta adresi alan ve bu e-postaya sahip kullanıcıyı
        // listeden bulan bir metot.
        public static UserDto? GetUserByEmail(string email)
        {
            return _users.FirstOrDefault(u =>
                string.Equals(u.Email, email, StringComparison.OrdinalIgnoreCase));
        }
    }
}
