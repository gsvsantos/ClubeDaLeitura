namespace ClubeDaLeitura.ModuloAmigo;

public class RepositorioAmigo
{
    public Amigo[] Amigos = new Amigo[100];
    public int IndiceListaAmigos = 0;

    public void RegistrarAmigo(Amigo novoAmigo)
    {
        novoAmigo.GerarId();
        Amigos[IndiceListaAmigos++] = novoAmigo;
    }
    public Amigo[] PegarListaRegistrados()
    {
        return Amigos;
    }
    public void EditarAmigo(Amigo amigoEscolhido, Amigo amigoEditado)
    {
        amigoEscolhido.Nome = amigoEditado.Nome;
        amigoEscolhido.Responsavel = amigoEditado.Responsavel;
        amigoEscolhido.Telefone = amigoEditado.Telefone;
    }
    public void ExcluirAmigo(Amigo amigoEscolhido)
    {
        for (int i = 0; i < Amigos.Length; i++)
        {
            if (Amigos[i] == null)
                continue;

            else if (Amigos[i].Id == amigoEscolhido.Id)
            {
                Amigos[i] = null!;
                break;
            }
        }
    }
    public Amigo SelecionarPorId(Amigo amigo)
    {
        foreach (Amigo a in Amigos)
        {
            if (a.Id == amigo.Id)
                return a;
        }
        return null!;
    }
}
