using MediatR;

namespace Application.Commands.Login
{
    /// <summary>
    /// Comando para realizar o login na aplicação
    /// </summary>
    public sealed class CreateLoginCommand : IRequest<CreateLoginCommandResult>
    {
        /// <summary>
        /// Nome do funcionário que irá submeter o arquivo OFX para processamento
        /// </summary>
        public string Funcionario { get; set; }

        /// <summary>
        /// Instituição que submeterá o arquivo para processamento
        /// </summary>
        public string Instituicao { get; set; }
    }
}