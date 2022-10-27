using AutoMapper;
using ExercicioAPIStella.Data.Context;
using ExercicioAPIStella.Data.Entities;
using ExercicioAPIStella.Domain.Contracts.Usuario;
using ExercicioAPIStella.Domain.Interfaces;
using ExercicioAPIStella.Domain.Profiles;
using ExercicioAPIStella.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Wilson.WebApi.Test.Services;

[TestClass]
public class UsuarioServiceTest
{
    private  IUsuarioService _service;
    private readonly Mock<IUsuarioRepository> _repositoryMock;
    private readonly IMapper _mapper;
    
    
    
    public UsuarioServiceTest()
    {
        _repositoryMock = new Mock<IUsuarioRepository>();
        
        var usuarioProfile = new UsuarioProfile();
        var configuration = new MapperConfiguration(prop => prop.AddProfile(usuarioProfile));
        _mapper = new Mapper(configuration);
    }
    
    [TestMethod]
    public async Task CadastraUsuarioComSucesso()
    {
        var novoUsuarioRequest = new UsuarioRequest()
        {
            Nome = "Leonor Valente",
            Telefone = "794-639-4982",
            Email = "Leonor.Valente71@hotmail.com",
            CPF = "024.516.478-25",
            Senha = "f?(Yk!|xHOlXr#g%1|z"
           
        };
        
        var novoUsuario = _mapper.Map<Usuario>(novoUsuarioRequest);
        
        
        _repositoryMock.Setup(repository => repository.AddAsync(novoUsuario));
        _service = new UsuarioService(_repositoryMock.Object,_mapper);

        
        
        await _service.CadastrarUsuario(novoUsuarioRequest);
        
    }
    
    [TestMethod]
    public async Task NaoPodeCadastrarUsuarioComCPFInvalido()
    {
        var novoUsuarioRequest = new UsuarioRequest()
        {
            Nome = "Leonor Valente",
            Telefone = "794-639-4982",
            Email = "Leonor.Valente71@hotmail.com",
            CPF = "024.516.478-25",
            Senha = "f?(Yk!|xHOlXr#g%1|z"
           
        };


        var novousuario = _mapper.Map<Usuario>(novoUsuarioRequest);

        _repositoryMock.Setup(repository => repository.AddAsync(novousuario));
        _service = new UsuarioService(_repositoryMock.Object, _mapper);
        
        
        
        novoUsuarioRequest.CPF = "0000000000";


        try
        {
            await _service.CadastrarUsuario(novoUsuarioRequest);
        }
        catch (Exception e)
        {
            Assert.AreEqual("CPF inválido.", e.Message);
        }

        
        




    }
}