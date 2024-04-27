namespace Domain.Entidades.Cuentas
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string EmailNormalizado { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
