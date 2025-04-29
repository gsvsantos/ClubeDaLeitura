using ClubeDaLeitura.Compartilhado;

namespace ClubeDaLeitura.ModuloReserva;

public class RepositorioReservaEmArquivo : RepositorioBaseEmArquivo<Reserva>, IRepositorioReserva
{
    public RepositorioReservaEmArquivo(ContextoDados contexto) : base(contexto) { }
    public override void CadastrarRegistro(Reserva novoRegistro)
    {
        novoRegistro.Revista.Reservar();
        novoRegistro.Amigo.ReceberReserva(novoRegistro);
        base.CadastrarRegistro(novoRegistro);
    }
    public bool VerificarReservaAtiva(Reserva reservaEscolhida)
    {
        if (reservaEscolhida.Status != "Ativa")
            return true;
        else
            return false;
    }
    protected override List<Reserva> ObterListaContexto()
    {
        return Contexto.RegistroReservas;
    }
}
