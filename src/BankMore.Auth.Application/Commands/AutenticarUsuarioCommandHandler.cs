 
using BankMore.Auth.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
  

namespace BankMore.Auth.Application.Commands
{
    public class AutenticarUsuarioCommandHandler : IRequestHandler<AutenticarUsuarioCommand, string>
    {
        private readonly IContaCorrenteRepository _repository;
        private readonly IConfiguration _config;

        public AutenticarUsuarioCommandHandler(IContaCorrenteRepository repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
        }

        public async Task<string> Handle(AutenticarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var conta = await _repository.ObterPorDocumentoOuNumeroAsync(request.DocumentoOuConta);
            if (conta is null)
                throw new UnauthorizedAccessException("Conta não encontrada");

            var hash = HashSenha(request.Senha, conta.Salt);
            if (conta.Senha != hash)
                throw new UnauthorizedAccessException("Senha inválida");

            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = _config["JwtSettings:SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim("id", conta.Id.ToString()),
                        new Claim("numero", conta.Numero.ToString())
                    }),
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(_config["JwtSettings:ExpiresInMinutes"])),  
                Issuer = _config["JwtSettings:Issuer"],
                Audience = _config["JwtSettings:Audience"],
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string HashSenha(string senha, string salt)
        {
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha + salt));
            return Convert.ToBase64String(hash);
        }
    }
}
