namespace jwt.serveces
{
    public interface iathuseves
    {
        Task<Athmodel> registerasync(Register user);
        Task<Athmodel> GETTOKEN(Tokenrquenst tok);
    }
}
