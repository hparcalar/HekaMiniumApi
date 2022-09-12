using HekaMiniumApi.Context;

namespace HekaMiniumApi.Business{
    public class OrderManagementBO : IDisposable{
        HekaMiniumSchema _context;
        public OrderManagementBO(HekaMiniumSchema context){
            _context = context;
        }

        public bool CheckOrderDetail(int orderDetailId){
            try
            {
                var dbObj = _context.ItemOrderDetail.FirstOrDefault(d => d.Id == orderDetailId);
                
                decimal? totalConsumed = _context.ItemOrderConsume.Where(d => d.ItemOrderDetailId == orderDetailId)
                    .Sum(d => (d.ConsumeNetQuantity ?? 0) + (d.ContributeNetQuantity ?? 0));
                
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