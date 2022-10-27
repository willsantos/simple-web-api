using ExercicioAPIStella.Data.Context;
using ExercicioAPIStella.Data.Entities;
using ExercicioAPIStella.Data.Repositories;
using ExercicioAPIStella.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Wilson.WebApi.Test.Repositories;

[TestClass]
public class UsuarioRepositoryTest
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IConfiguration _configuration;


    public UsuarioRepositoryTest()
    {
        
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<IUsuarioRepository, UsuarioRepository>();
        serviceCollection.AddDbContext<ApplicationDbContext>(
            options => options.UseSqlServer(@"Server=127.0.0.1,1439;Database=stellaApi_tests;User=SA;Password=wd%!r$H7Ez@yuPz*sx*3wUgBcwv;")
            );
        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        _usuarioRepository = serviceProvider.GetRequiredService<IUsuarioRepository>();
    }

    [TestMethod]
    public async Task CadastraUsuarioComSucessoTest()
    {
        var novoUsuario = new Usuario()
        {
            Nome = "Leonor Valente",
            Telefone = "794-639-4982",
            Email = "Leonor.Valente71@hotmail.com",
            CPF = "024.516.478-25",
            Senha = "f?(Yk!|xHOlXr#g%1|z"
        };


        await _usuarioRepository.AddAsync(novoUsuario);
        var usuarioAssert = await _usuarioRepository.FindAsync(novoUsuario.UsuarioId);

        Assert.AreEqual(usuarioAssert, novoUsuario);
    }
}