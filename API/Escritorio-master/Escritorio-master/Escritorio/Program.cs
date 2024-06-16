using Escritorio.Shared.Dados.Banco;

public class Program
{
    static void Main(string[] args)
    {
        var context = new EscritorioContext();
        Console.WriteLine("Olá, mundo!");
    }
}