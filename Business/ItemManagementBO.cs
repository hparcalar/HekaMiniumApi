using HekaMiniumApi.Context;

namespace HekaMiniumApi.Business{
    public class ItemManagementBO : IDisposable {
        HekaMiniumSchema _context;
        public ItemManagementBO(HekaMiniumSchema context){
            _context = context;
        }

        public bool CheckReceiptDetail(int receiptDetailId){
           try
            {
                var dbObj = _context.ItemReceiptDetail.FirstOrDefault(d => d.Id == receiptDetailId);
                
                decimal? totalConsumed = _context.ItemReceiptConsume.Where(d => d.ConsumedReceiptDetailId == receiptDetailId)
                    .Sum(d => (d.ConsumeNetQuantity ?? 0));
                
                if (dbObj.Quantity > totalConsumed)
                    dbObj.ReceiptStatus = 0; // to be created
                else if (dbObj.Quantity <= totalConsumed)
                    dbObj.ReceiptStatus = 2; // to be completed
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        } 

        public void Dispose(){

        }
    }
}