using Microsoft.AspNetCore.Mvc;

namespace MyApi.Controllers;

[ApiController]
[Route("[controller]")] // URL: SuperHero/...
public class SuperHeroController : ControllerBase
{
    [HttpGet("superman")] // URL: SuperHero/superman
    public Superhero GetFirst()
    {
        return Superhero.Superman;
    }

    [HttpGet("batman")] // URL: SuperHero/batman
    public Superhero GetSecond()
    {
        return Superhero.Batman.Value;
    }
}

/* TODO:
 * - Только 2 супергероя, каждый в единственном экземпляре
 * - Thread.Sleep в конструкторе не удалять
 * - Подсказка: по сути нужно сделать так, чтобы не приходилось на каждый вызов контроллера ждать 5 секунд
 */
public class Superhero
{
    public string Name { get; }

    private Superhero(string name)
    {
        Name = name;
        Thread.Sleep(5000);
    }

    private static Superhero? superman;
    public static Superhero Superman => superman ??= new Superhero("Superman");

    // Если отдать наружу Lazy, то можно более явно обозначить, что значение инициализируется лениво
    public static readonly Lazy<Superhero> Batman = new(() => new Superhero("Batman"));
}