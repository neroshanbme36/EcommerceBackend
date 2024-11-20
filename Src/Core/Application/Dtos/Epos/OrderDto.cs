using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Enums.CloudStoreEpos;

namespace Application.Dtos.CloudStoreEpos.Epos
{
    public class OrderDto
    {
        public OrderHeaderDto Header { get; set; } = new OrderHeaderDto();
        public List<ParentProdLineOrderDto> ParentProductLines { get; set; } = new List<ParentProdLineOrderDto>();
        public OrderSummaryDto OrderSummary { get; set; } = new OrderSummaryDto();

        public OrderDto(OrderHeaderDto header, IReadOnlyList<OrderLineDto> lines)
        {
            Header = header;
            OrderSummary = new OrderSummaryDto();
            if (lines != null && lines.Count > 0)
            {
                lines = lines.OrderBy(c => c.LineNo).ToList();
                List<OrderLineDto> parentProdLines = lines.Where(x => !x.IsModifier).ToList();
                List<OrderLineDto> modProdLines = lines.Where(x => x.IsModifier).ToList();

                int serialNo = 1;
                foreach (OrderLineDto parentProd in parentProdLines)
                {
                    #region Fill ParentProdLines
                    ParentProdLineOrderDto parentProdLine = new ParentProdLineOrderDto();
                    parentProdLine.SerialNo = serialNo;
                    parentProdLine.LineNo = parentProd.LineNo;
                    parentProdLine.EntryType = parentProd.EntryType;
                    parentProdLine.TransType = parentProd.TransType;
                    parentProdLine.KeyId = parentProd.KeyId;
                    parentProdLine.Description = parentProd.Description;
                    parentProdLine.Quantity = parentProd.Quantity;
                    parentProdLine.Price = parentProd.Price;
                    parentProdLine.DiscountPercentage = parentProd.DiscountPercentage;
                    parentProdLine.DiscountAmount = parentProd.DiscountAmount;
                    parentProdLine.Amount = parentProd.Amount;
                    parentProdLine.NetAmount = parentProd.NetAmount;
                    parentProdLine.FreeText = parentProd.FreeText;
                    parentProdLine.LineStatus = parentProd.LineStatus;
                    parentProdLine.Barcode = parentProd.Barcode;
                    parentProdLine.Scanned = parentProd.Scanned;
                    parentProdLine.CreatedOn = parentProd.CreatedOn;
                    parentProdLine.CreatedOnGmt = parentProd.CreatedOnGmt;

                    List<OrderLineDto> allModProds = modProdLines.Where(x => x.ParentKeyId == parentProd.ParentKeyId
                        && x.ParentLineNo == parentProd.LineNo).ToList();

                    if (allModProds.Count > 0)
                    {
                        var modifierHeaders = allModProds.GroupBy(c => c.ModifierHeaderId).Select(g => new
                        {
                            modifierHeaderId = g.Key,
                            modifierTitle = g.Max(a => a.ModifierTitle),
                            totalPrice = g.Sum(s => s.Price),
                            totalAmount = g.Sum(s => s.Amount),
                            totalNetAmt = g.Sum(s => s.NetAmount)
                        }).ToList();

                        parentProdLine.Price += modifierHeaders.Sum(x => x.totalPrice);
                        parentProdLine.Amount += modifierHeaders.Sum(x => x.totalAmount);
                        parentProdLine.NetAmount += modifierHeaders.Sum(x => x.totalNetAmt);

                        foreach (var modHeader in modifierHeaders)
                        {
                            ModifierHeaderOrderDto modifierHeader = new ModifierHeaderOrderDto();
                            modifierHeader.ModifierTitle = modHeader.modifierTitle;
                            modifierHeader.TotalAmount = modHeader.totalPrice;

                            List<OrderLineDto> subModProds = allModProds.Where(x => x.ModifierHeaderId == modHeader.modifierHeaderId).ToList();
                            foreach (OrderLineDto sub in subModProds)
                            {
                                SubModifierProdLineOrderDto smp = new SubModifierProdLineOrderDto();
                                smp.Description = sub.Description;
                                smp.Price = sub.Price;
                                modifierHeader.SubModifierProducts.Add(smp);
                            }
                            parentProdLine.ModifierHeaders.Add(modifierHeader);
                        }
                    }
                    #endregion Fill mainProdLine
                    if (!parentProdLine.LineStatus)
                    {
                        if (parentProdLine.EntryType == EntryType.Product)
                        {
                            if (!parentProdLine.KeyId.ToLower().Equals("tdisc"))
                            {
                                OrderSummary.NoOfProducts += parentProdLine.Quantity;
                            }
                        }
                        OrderSummary.CustomerOwes += parentProdLine.NetAmount;
                        OrderSummary.Discount += parentProdLine.DiscountAmount;
                        // if (parentProdLine.EntryType == 0 || parentProdLine.EntryType == 1)
                        // {
                        //   subTotalSales += parentProdLine.NetAmount;
                        // }
                        if (parentProdLine.EntryType != EntryType.Payment)
                        {
                            OrderSummary.Total += parentProdLine.NetAmount;
                        }
                        else if (parentProdLine.EntryType == EntryType.Payment)
                        {
                            if (!parentProdLine.KeyId.Equals("6") && !parentProdLine.Description.ToLower().Equals("change"))
                            {
                                OrderSummary.Payment += parentProdLine.NetAmount;
                            }
                            else
                            {
                                OrderSummary.Balance += parentProdLine.NetAmount;
                            }
                        }
                    }
                    ParentProductLines.Add(parentProdLine);
                    serialNo++;
                }
            }
        }
    }

    public class ParentProdLineOrderDto
    {
        public int SerialNo { get; set; }
        public int LineNo { get; set; }
        public EntryType EntryType { get; set; }
        public TransactionType TransType { get; set; }
        public string KeyId { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Amount {get; set;}
        public decimal NetAmount { get; set; }
        public string FreeText { get; set; }
        public bool LineStatus { get; set; }
        public string Barcode { get; set; }
        public bool Scanned { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime CreatedOnGmt { get; set; }
        public string? ThumbnailUrl { get; set; }
        public List<ModifierHeaderOrderDto> ModifierHeaders { get; set; } = new List<ModifierHeaderOrderDto>();
    }

    public class ModifierHeaderOrderDto
    {
        public string ModifierTitle { get; set; }
        public decimal TotalAmount { get; set; }
        public List<SubModifierProdLineOrderDto> SubModifierProducts { get; set; } = new List<SubModifierProdLineOrderDto>();
    }

    public class SubModifierProdLineOrderDto
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    public class OrderSummaryDto
    {
        public decimal Total { get; set; }
        public decimal CustomerOwes { get; set; }
        public decimal Discount { get; set; }
        public decimal Payment { get; set; }
        public decimal Balance { get; set; }
        public decimal NoOfProducts { get; set; }
    }
}