using Subsembly.SmartCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFIDReader
{
    public class CardHelper
    {
        CardHandle m_aCard = null;
        bool m_isContactless = false;
        bool m_isProxViaATR = false; // HID PROX via OMNIKEY 5x25
        string m_cardInfo = "card info:\n";

        public CardHelper(CardHandle aCard)
        {
            if (aCard == null)
            {
                throw new ArgumentNullException("invalid card handle");
            }
            m_aCard = aCard;
            CardPcscPart3 pcscPart3Info = new CardPcscPart3(aCard.GetATR());
            if (pcscPart3Info.isValid)
            {
                m_isContactless = pcscPart3Info.isContactless;
                m_cardInfo += "PC/SC 2.01 compliant ATR detected\n";
                m_cardInfo += "interface:  ";
                if (m_isContactless == true)
                {
                    m_cardInfo += "contactless\n";
                }
                else
                {
                    m_cardInfo += "contact\n";
                }
                m_cardInfo += "protocol:  " + pcscPart3Info.CardProtocol + "\n";
                m_cardInfo += "card type: " + pcscPart3Info.CardName + "\n";
            }
            else
            {
                //m_cardInfo += "ATR is not PC/SC 2.01 part 3 compliant\n";
                //m_cardInfo += "processor card or proprietary storage card\n";
            }




            // at this time we know: it is NOT a contactless or contact storage card as defined in PC/SC part 3
            // Let's try to find out more by analyzing the reader name. We may get lucky. Note though, that there is 
            // no clean, standards-based way to determine the card interface this way.

            if (m_isContactless == false)
            {
                if (aCard.Slot.CardTerminalName.Contains("OMNIKEY") &&
                   aCard.Slot.CardTerminalName.Contains("CL"))
                {
                    if (aCard.Slot.CardTerminalName.Contains("5x25"))
                    {
                        m_isProxViaATR = true;
                        m_cardInfo += "125 KHz PROX via ATR historical bytes\n";
                    }
                    else
                    {
                        m_isContactless = true;
                        m_cardInfo += "contactless based on reader name\n";
                    }
                }
            }

            if (m_isContactless == false)
            {

                if (aCard.Slot.CardTerminalName.ToUpper().Contains("CONTACTLESS") == true) // SCM readers contain this string
                {
                    m_isContactless = true;
                    m_cardInfo += "contactless based on reader name\n";
                }
            }

            if (m_isContactless == false && m_isProxViaATR == false)
            {

                // we could start an analysis based onm an ATR mask here
                m_cardInfo += "unknown card interface (fall back)\n";
            }

            // let us know if you are using a reader that could be handled here
            // support@smartcard-api.com
            m_cardInfo += "end of card info ";
        }

        /// </summary>
        /// <param name="aCard"></param>
        /// <returns></returns>
        public bool isContactless
        {
            get
            {
                return m_isContactless;
            }
        }

        /// </summary>
        /// <param name="aCard"></param>
        /// <returns></returns>
        public bool isProprietaryProx
        {
            get
            {
                return m_isProxViaATR;
            }
        }

        public string cardInfo
        {
            get
            {
                return m_cardInfo;
            }
        }

    } // class
}
