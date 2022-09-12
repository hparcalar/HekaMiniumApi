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