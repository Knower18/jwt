
using jwt.APIHELP;
using jwt.model;
using Microsoft.AspNetCore.Identity;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace jwt.serveces
{
    public class Athuserviece : iathuseves
    {
        private readonly UserManager<user> user1;
        private readonly jwtt _jwt;
        public Athuserviece(UserManager<user> user,IOptions<jwtt> options )
        {
            this.user1 = user;  
            this._jwt = options.Value;

            
        }
        public async Task<Athmodel> registerasync(Register user)
        {
          if( await user1.FindByEmailAsync(user.email) != null)
            {
                return new Athmodel { message = "this mail is already exist" };
            }
            if (await user1.FindByNameAsync(user.usernmae) != null)
            {
                return new Athmodel { message = "this username is already exist" };
            }
            var userr = new user
            {
                lastname = user.lastname,
                Name = user.fistname,
                UserName = user.usernmae,
                Email = user.email,
            };
            var result =await user1.CreateAsync(userr,user.password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += error.Description;
                }
                return new Athmodel { message = errors };
            }
            await user1.AddToRoleAsync(userr, "User");
            var jwtsecurtytoke = await creattoken(userr);
            return new Athmodel
            {
                email=userr.Email,
                expire=jwtsecurtytoke.ValidTo,
                isathu=true,
                roles= new List<string> { "user"},
                token=new  JwtSecurityTokenHandler().WriteToken(jwtsecurtytoke),
                name=userr.UserName
            };
        }
        public async Task<Athmodel> GETTOKEN(Tokenrquenst tok)
        {
            var athmodel = new Athmodel();
            var user = await user1.FindByEmailAsync(tok.mail);
            if (user == null || !await user1.CheckPasswordAsync(user, tok.password))
            {
                athmodel.message = "email  or password is worng ";
                return athmodel;
            }
            athmodel.isathu = true;
            var jwttoen = await creattoken(user);
            athmodel.token=new JwtSecurityTokenHandler().WriteToken(jwttoen);
            athmodel.email = user.Email;
            athmodel.name = user.UserName;
            athmodel.expire=jwttoen.ValidTo;
            var roles = await user1.GetRolesAsync(user);
            athmodel.roles = roles.ToList();
            

            return athmodel;
        }
        public async Task<JwtSecurityToken> creattoken(user users)
        {
            var usercalims = await user1.GetClaimsAsync(users);
            var roles = await user1.GetRolesAsync(users);
            var rolecalims = new List<Claim>();
            foreach (var role in roles)
            {
                rolecalims.Add(new Claim("roles", role));
            }
            var calmisx = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,users.UserName),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
               new Claim(JwtRegisteredClaimNames.Email,users.Email),
               new Claim("USER_ID",users.Id)
            }.Union(usercalims).Union(rolecalims);
            var seymatricseutyrykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.key));
            var signingCredentialsd = new SigningCredentials(seymatricseutyrykey, SecurityAlgorithms.HmacSha256);
            var jwttoekn = new JwtSecurityToken(
                issuer: _jwt.issuer,
                audience:_jwt.Aud,
                 claims:calmisx,
                 expires:DateTime.Now.AddDays(_jwt.Durtaion),
                 signingCredentials:signingCredentialsd




                );
            
              return jwttoekn;
        }
    }
}
