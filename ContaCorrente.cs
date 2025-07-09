public class ContaCorrente
{
    public Guid Id { get; private set; }
    public int Numero { get; private set; }
    public string Nome { get; private set; }
    public bool Ativo { get; private set; }
    public string Senha { get; private set; }
    public string Salt { get; private set; }

    public void Desativar()
    {
        Ativo = false;
    }
}
