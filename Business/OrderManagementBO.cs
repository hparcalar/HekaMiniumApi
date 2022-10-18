using HekaMiniumApi.Context;

namespace HekaMiniumApi.Business{
    public class OrderManagementBO : IDisposable{
        HekaMiniumSchema _context;
        public OrderManagementBO(HekaMiniumSchema context){
            _context = context;
        }

        public bool CheckDemandDetail(int demandDetailId){
            try
            {
                var dbObj = _context.ItemDemandDetail.FirstOrDefault(d => d.Id == demandDetailId);

                if (_context.ItemDemandConsume.Any(d => d.ItemDemandDetailId == demandDetailId && d.ItemOrderDetailId != null)){
                    dbObj.DemandStatus = 2; // to be ordered
                    if (_context.ItemDemandConsume.Any(d => d.ItemDemandDetailId == demandDetailId && d.ItemOrderDetail.ReceiptStatus == 2))
                        dbObj.DemandStatus = 6; // to be order forwarded
                    
                    var incomingQuantity = (_context.ItemReceiptDetail.Where(d => d.ItemDemandDetailId == demandDetailId && d.ItemReceipt.ReceiptType < 100).Select(d => d.Quantity).Sum() ?? 0);
                    if (dbObj.Quantity > incomingQuantity && incomingQuantity > 0)
                        dbObj.DemandStatus = 7; // to be partially received
                    else if (dbObj.Quantity <= incomingQuantity && incomingQuantity > 0)
                        dbObj.DemandStatus = 3; // to be completely received
                }
                else if (_context.ItemOfferDetailDemand.Any(d => d.ItemDemandDetailId == demandDetailId))
                    dbObj.DemandStatus = 5; // to be offered
                else
                    dbObj.DemandStatus = 0;
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }

        public bool CheckOfferDetail(int offerDetailId){
            try
            {
                var dbObj = _context.ItemOfferDetail.FirstOrDefault(d => d.Id == offerDetailId);
                
                decimal? totalConsumed = _context.ItemOrderDetail.Where(d => d.ItemOfferDetailId == offerDetailId)
                    .Sum(d => d.Quantity);

                if (dbObj.Quantity > totalConsumed)
                    dbObj.OfferStatus = 0;
                else if (dbObj.Quantity <= totalConsumed)
                    dbObj.OfferStatus = 3;
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }

        public bool CheckOrderDetail(int orderDetailId){
            try
            {
                var dbObj = _context.ItemOrderDetail.FirstOrDefault(d => d.Id == orderDetailId);
                
                decimal? orderConsumings = _context.ItemOrderConsume.Where(d => d.ItemOrderDetailId == orderDetailId)
                    .Sum(d => (d.ConsumeNetQuantity ?? 0) + (d.ContributeNetQuantity ?? 0));

                var demandConsumes = _context.ItemDemandConsume.Where(d => d.ItemOrderDetailId == orderDetailId).ToArray();
                int demandSatisfyStatus = 3;

                if (demandConsumes.Length == 0)
                    demandSatisfyStatus = 0;

                foreach (var demand in demandConsumes)
                {
                    decimal? consumedByReceipts = _context.ItemReceiptDetail.Where(d => d.ItemOrderDetailId == orderDetailId
                        && d.ItemDemandDetailId == demand.ItemDemandDetailId).Select(d => d.Quantity).Sum() ?? 0;

                    var dbDemandDetail = _context.ItemDemandDetail.FirstOrDefault(d => d.Id == demand.ItemDemandDetailId);
                    
                    if ((consumedByReceipts ?? 0) <= 0){
                        demandSatisfyStatus = 0;
                        break;
                    }
                    else if (dbDemandDetail.Quantity > consumedByReceipts)
                    {
                        demandSatisfyStatus = 5;
                        break;
                    }
                }

                if (orderConsumings > 0){
                    if (dbObj.Quantity > orderConsumings)
                        dbObj.ReceiptStatus = dbObj.ReceiptStatus > 2 ? 0 : dbObj.ReceiptStatus; // to be created, approved or sent to supplier status
                    else if (dbObj.Quantity <= orderConsumings)
                        dbObj.ReceiptStatus = 3; // to be completed
                }
                else{
                    if (demandSatisfyStatus == 0)
                        dbObj.ReceiptStatus = dbObj.ReceiptStatus > 2 ? 0 : dbObj.ReceiptStatus; // to be created, approved or sent to supplier status
                    else if (demandSatisfyStatus == 3)
                        dbObj.ReceiptStatus = 3; // to be completed
                    else if (demandSatisfyStatus == 5)
                        dbObj.ReceiptStatus = 5; // to partially completed
                }
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        } 

        public bool CheckOrderHeader(int orderId){
            try
            {
                var dbObj = _context.ItemOrder.FirstOrDefault(d => d.Id == orderId);
                if (dbObj != null){
                    if (_context.ItemOrderDetail.Any(d => d.ReceiptStatus == 3 && d.ItemOrderId == orderId) && !_context.ItemOrderDetail.Any(d => d.ReceiptStatus != 3 && d.ItemOrderId == orderId)){
                        dbObj.ReceiptStatus = 3;
                    }
                    else if (_context.ItemOrderDetail.Any(d => d.ReceiptStatus == 4 && d.ItemOrderId == orderId) && !_context.ItemOrderDetail.Any(d => d.ReceiptStatus != 4 && d.ItemOrderId == orderId)){
                        dbObj.ReceiptStatus = 4;
                    }
                    else if (_context.ItemOrderDetail.Any(d => d.ReceiptStatus == 2 && d.ItemOrderId == orderId) && !_context.ItemOrderDetail.Any(d => d.ReceiptStatus != 2 && d.ItemOrderId == orderId)){
                        dbObj.ReceiptStatus = 2;
                    }
                    else if (_context.ItemOrderDetail.Any(d => d.ReceiptStatus == 1 && d.ItemOrderId == orderId) && !_context.ItemOrderDetail.Any(d => d.ReceiptStatus != 1 && d.ItemOrderId == orderId)){
                        dbObj.ReceiptStatus = 1;
                    }
                    else if (_context.ItemOrderDetail.Any(d => d.ReceiptStatus == 5 && d.ItemOrderId == orderId) && !_context.ItemOrderDetail.Any(d => d.ReceiptStatus != 5 && d.ItemOrderId == orderId)){
                        dbObj.ReceiptStatus = 5;
                    }
                    else
                        dbObj.ReceiptStatus = 0;
                }
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }

        public bool CheckDemandHeader(int demandId){
            try
            {
                var dbObj = _context.ItemDemand.FirstOrDefault(d => d.Id == demandId);
                if (dbObj != null){
                    if (_context.ItemDemandDetail.Any(d => d.DemandStatus == 2 && d.ItemDemandId == demandId) && !_context.ItemDemandDetail.Any(d => d.DemandStatus != 2 && d.ItemDemandId == demandId)){
                        dbObj.DemandStatus = 2;
                    }
                    else if (_context.ItemDemandDetail.Any(d => d.DemandStatus == 6 && d.ItemDemandId == demandId) && !_context.ItemDemandDetail.Any(d => d.DemandStatus != 6 && d.ItemDemandId == demandId)){
                        dbObj.DemandStatus = 6;
                    }
                    else if (_context.ItemDemandDetail.Any(d => d.DemandStatus == 7 && d.ItemDemandId == demandId) && !_context.ItemDemandDetail.Any(d => d.DemandStatus != 7 && d.ItemDemandId == demandId)){
                        dbObj.DemandStatus = 7;
                    }
                    else if (_context.ItemDemandDetail.Any(d => d.DemandStatus == 3 && d.ItemDemandId == demandId) 
                        && !_context.ItemDemandDetail.Any(d => d.DemandStatus != 3 && d.ItemDemandId == demandId)){
                        dbObj.DemandStatus = 3;
                    }
                    else
                        dbObj.DemandStatus = 0;
                }
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }

        public bool CheckOfferHeader(int offerId){
            try
            {
                var dbObj = _context.ItemOffer.FirstOrDefault(d => d.Id == offerId);
                if (dbObj != null){
                    if (_context.ItemOfferDetail.Any(d => d.OfferStatus == 3 && d.ItemOfferId == offerId) &&
                        !_context.ItemOfferDetail.Any(d => d.OfferStatus != 3 && d.ItemOfferId == offerId))
                        dbObj.OfferStatus = 3;
                    else
                        dbObj.OfferStatus = 0;
                }
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