using ClubeDaLeitura.ModuloCaixa;

namespace ClubeDaLeitura.ModuloRevista;

public class Revista
{
    public string Titulo;
    public int NumeroEdicao;
    public int AnoPublicacao;
    public string StatusEmprestimo;
    public Caixa Caixa;
    private static int id = 0;
}
