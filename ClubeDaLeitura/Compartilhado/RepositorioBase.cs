namespace ClubeDaLeitura.Compartilhado;

public abstract class RepositorioBase
{
    public EntidadeBase[] Registros { get; protected set; } = new EntidadeBase[100];
    private int id = 0;
    public bool ListaVazia { get; set; } = false;

    public virtual void CadastrarRegistro(EntidadeBase novoRegistro)
    {
        novoRegistro.Id = ++id;
        InserirRegistro(novoRegistro);
    }
    public EntidadeBase[] PegarListaRegistrados()
    {
        return Registros;
    }
    public void EditarRegistro(EntidadeBase registroEscolhido, EntidadeBase dadosEditados)
    {
        registroEscolhido.AtualizarRegistro(dadosEditados);
    }
    public void ExcluirRegistro(EntidadeBase registroEscolhido)
    {
        for (int i = 0; i < Registros.Length; i++)
        {
            if (Registros[i] == null)
                continue;
            else if (Registros[i].Id == registroEscolhido.Id)
            {
                Registros[i] = null!;
                break;
            }
        }
    }
    public EntidadeBase SelecionarRegistroPorId(int idRegistroEscolhida)
    {
        foreach (EntidadeBase e in Registros)
        {
            if (e == null)
                continue;

            if (e.Id == idRegistroEscolhida)
                return e;
        }

        return null!;
    }
    protected void InserirRegistro(EntidadeBase registro)
    {
        for (int i = 0; i < Registros.Length; i++)
        {
            if (Registros[i] == null)
            {
                Registros[i] = registro;
                return;
            }
        }
    }
}
