using LotoTest.Application;

namespace LotoTest.Persistence
{
    public class LottoRepository : ILottoRepository
    {
        private readonly LottoDbContext _lottoDbContext;

        public LottoRepository(LottoDbContext lottoDbContext)
        {
            this._lottoDbContext = lottoDbContext;
        }

        public void Add(LotteryCombination lotteryCombination)
        {
            _lottoDbContext.Add(lotteryCombination);
            _lottoDbContext.SaveChanges();
        }
    }
}
