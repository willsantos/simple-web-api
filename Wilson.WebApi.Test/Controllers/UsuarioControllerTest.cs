using ExercicioAPIStella.Controllers;
using ExercicioAPIStella.Domain.Contracts.Usuario;
using ExercicioAPIStella.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Wilson.WebApi.Test.Controllers;

[TestClass]
public class UsuarioControllerTest
{
    private readonly Mock<IUsuarioService> _serviceMock;
    
    public UsuarioControllerTest()
    {
        _serviceMock = new Mock<IUsuarioService>();
    }
    
    
    [TestMethod]
    public async  Task RotaCadastraUsuarioDeveRetornarStatusCode201Test()
    {
        
        var novoUsuarioRequest = new UsuarioRequest()
        {
            Nome = "Leonor Valente",
            Telefone = "794-639-4982",
            Email = "Leonor.Valente71@hotmail.com",
            CPF = "024.516.478-25",
            Senha = "f?(Yk!|xHOlXr#g%1|z"
        };


        _serviceMock.Setup(prop => prop.CadastrarUsuario(novoUsuarioRequest));
        
        var controller = new UsuarioController(_serviceMock.Object);



        var result = await controller.Post(novoUsuarioRequest);

        var okResult = result as CreatedResult;
        
        Assert.IsNotNull(okResult);
        Assert.AreEqual(201,okResult.StatusCode);
    }
}