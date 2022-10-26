using Acg.University.BL.Contratos;
using Acg.University.DAL.Entidades;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UniversityGRPC.Services
{
    public class UniversityService : University.UniversityBase
    {
        private readonly IConfiguration _conf;
        private readonly ILogger<UniversityService> _logger;
        private readonly IServUsuarios _sUsuarios;
        private readonly IServPersonas _sPersonas;
        
        public UniversityService(
            ILogger<UniversityService> logger,
            IConfiguration configuration,
            IServUsuarios sUsuarios,
            IServPersonas sPersonas)
        {
            _conf = configuration;
            _logger = logger;
            _sUsuarios = sUsuarios;
            _sPersonas = sPersonas;
        }

        //  rpc Login (Credenciales) returns (JwtToken);
        public override Task<JwtToken> Login(Credenciales cred, ServerCallContext context)
        {
            string tk = "";
            var usr = _sUsuarios.Login(cred.Login, cred.Passwd);
            if (usr != null)
            {
                var c = new List<Claim> {
                        new Claim(ClaimTypes.Name, usr.Login),
                        new Claim(ClaimTypes.NameIdentifier, usr.Id.ToString())
                    };
                foreach (var r in usr.Roles)
                    c.Add(new Claim(ClaimTypes.Role, r.Nombre));

                var secKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf["jwt:Key"]));
                var crd = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256Signature);

                var tokenDesc = new JwtSecurityToken(
                    issuer: _conf["jwt:Issuer"],
                    audience: _conf["jwt:Audience"],
                    claims: c,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddMinutes(120),
                    signingCredentials: crd);

                tk = new JwtSecurityTokenHandler().WriteToken(tokenDesc);
            }

            return Task.FromResult(new JwtToken
            {
                Token = tk
            });
        }

        // [Authorize]
        public override Task<ListaAdmin> ListaAdministradores(Nada info, ServerCallContext context)
        {
            var l = new ListaAdmin();

            var l2 = _sPersonas.ListaAdministradores().Select(x => new AdministradorR()
            {
                Id = x.Id,
                IdPersona = x.PersonaId,
                Dni = x.Persona.DNI,
                Direc = x.Persona.Direccion,
                Nombre = x.Persona.Nombre,
                Poblac = x.Persona.Poblacion,
                Prov = x.Persona.Provincia
            }).ToList();

            l.Lista.Add(l2);

            return Task.FromResult(l);
        }

        [Authorize]
        public override async Task<Nada> EnviarMsgChat(Texto msg, ServerCallContext context)
        {
            var cnx = new HubConnectionBuilder()
                .WithUrl("https://localhost:7119/HubPrueba")
                .Build();

            await cnx.StartAsync();

            var txtUsr = context.GetHttpContext().User?.Identity?.Name ?? "";

            try
            {
                await cnx.InvokeAsync("EnviarMsg", txtUsr, msg.Valor);
                //await cnx.InvokeAsync("EnviarMsg2", msg.Valor);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // return Task.FromResult(new Nada());
            return new Nada();
        }
    }
}
