﻿using Domain.Entidades.Cuentas;
using Microsoft.AspNetCore.Identity;
using Repository.IRepositories.Cuentas;

namespace Services.Services.Usuarios
{
    public class UsuarioStoreService : IUserStore<Usuario?>, IUserEmailStore<Usuario?>, IUserPasswordStore<Usuario?>
    {
        private readonly IUsuariosRepository _usuariosRepository;

        public UsuarioStoreService(IUsuariosRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
        }

        public async Task<IdentityResult> CreateAsync(Usuario? user, CancellationToken cancellationToken)
        {
            if(user is not null)
            {
                user.Id = await _usuariosRepository.CrearUsuario(user);
            }
            return IdentityResult.Success;
        }

        public Task<IdentityResult> DeleteAsync(Usuario? user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public async Task<Usuario?> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return await _usuariosRepository.BuscarUsuarioPorEmail(normalizedEmail);
        }

        public Task<Usuario?> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return await _usuariosRepository.BuscarUsuarioPorEmail(normalizedUserName);
        }

        public Task<string?> GetEmailAsync(Usuario? user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user?.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(Usuario? user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedEmailAsync(Usuario? user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(Usuario? user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string?> GetPasswordHashAsync(Usuario? user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user?.PasswordHash);
        }

        public Task<string?> GetUserIdAsync(Usuario? user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user?.Id.ToString());
        }

        public Task<string?> GetUserNameAsync(Usuario? user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user?.Email);
        }

        public Task<bool> HasPasswordAsync(Usuario? user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(Usuario? user, string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(Usuario? user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedEmailAsync(Usuario? user, string normalizedEmail, CancellationToken cancellationToken)
        {
            if(user is not null)
            {
                user.EmailNormalizado = normalizedEmail;
            }
            return Task.CompletedTask;
        }

        public Task SetNormalizedUserNameAsync(Usuario? user, string normalizedName, CancellationToken cancellationToken)
        {
            if(user is not null)
            {
                user.EmailNormalizado = normalizedName;
            }
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(Usuario? user, string passwordHash, CancellationToken cancellationToken)
        {
            if(user is not null )
            {
                user.PasswordHash = passwordHash;
            }
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(Usuario? user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(Usuario? user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
