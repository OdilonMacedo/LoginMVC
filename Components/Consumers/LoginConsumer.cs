using Contracts;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Consumers
{
    public class LoginConsumer : IConsumer<ILoginRequest>
    {
        public async Task Consume(ConsumeContext<ILoginRequest> context)
        {
            if (context.Message.Username == "six" && context.Message.Password == "123")
            {
                await context.RespondAsync<ILoginResponse>(new
                {
                    Message = "Logado com sucesso"
                });
            }
            else if (context.Message.Username == "six" && context.Message.Password != "123")
            {
                await context.RespondAsync<ILoginNotFound>(new
                {
                    Message = "Senha incorreta"
                });
            }else
            {
                await context.RespondAsync<ILoginNotFound>(new
                {
                    Message = "Usuário incorreto"
                });
            }
        }
    }
}
