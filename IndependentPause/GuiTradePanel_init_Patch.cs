using System;
using HarmonyLib;
using UnityEngine;
using MadrugaShared;
using DawnOfMan;
using System.Reflection;

namespace IndependentPause
{

    [HarmonyPatch(typeof(GuiTradePanel), "init", MethodType.Normal)]
    static class GuiTradePanel_init_Patch
    {

        static MethodInfo method_refreshLayout
            = AccessTools.DeclaredMethod(typeof(GuiTradePanel), "refreshLayout");

        static MethodInfo method_onClose
            = AccessTools.DeclaredMethod(typeof(GuiTradePanel), "onClose");

        static bool Prefix(GuiTradePanel __instance, Trader trader, ProductAmounts settlementProducts)
        {
            Traverse t = Traverse.Create(__instance);
            t.Field<Trader>("mTrader").Value = trader;
            t.Field<GuiGridLayout>("mMainLayout").Value = new GuiGridLayout(1);
            t.Field<ProductAmounts>("mTraderStock").Value = trader.getTradeProducts().clone();
            t.Field<ProductAmounts>("mTraderTrade").Value = new ProductAmounts();
            t.Field<ProductAmounts>("mSettlementStock").Value = settlementProducts.clone();
            t.Field<ProductAmounts>("mSettlementTrade").Value = new ProductAmounts();
            MethodInvoker.GetHandler(method_refreshLayout)(__instance, null);
            t.Field<GuiPanel>("mPanel").Value = Singleton<GuiManager>.Instance.createPanel(t.Field<GuiGridLayout>("mMainLayout").Value, StringList.get("trade"), AlignmentFlags.None, GuiPanelFlags.CloseButton | GuiPanelFlags.Modal);
            t.Field<GuiPanel>("mPanel").Value.setOnCloseCallback(() => MethodInvoker.GetHandler(method_onClose)(__instance, null));
            t.Field<GuiPanel>("mPanel").Value.setTitleColor(GuiSettings.Instance.TextColorTitles);
            t.Field<GuiPanel>("mPanel").Value.setCloseButtonColor(GuiSettings.Instance.ButtonIconColor);
            Singleton<GuiManager>.Instance.clearToast();
            return false;
        }
    }

}
