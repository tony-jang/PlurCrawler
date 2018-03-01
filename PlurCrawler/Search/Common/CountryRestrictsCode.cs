using PlurCrawler.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Search.Common
{
    public enum CountryRestrictsCode
    {
        [Description("제한 없음")]
        All,

        [Description("Afghanistan")]
        countryAF,

        [Description("Albania")]
        countryAL,

        [Description("Algeria")]
        countryDZ,

        [Description("American Samoa")]
        countryAS,

        [Description("Andorra")]
        countryAD,

        [Description("Angola")]
        countryAO,

        [Description("Anguilla")]
        countryAI,

        [Description("Antarctica")]
        countryAQ,

        [Description("Antigua and Barbuda")]
        countryAG,

        [Description("Argentina")]
        countryAR,

        [Description("Armenia")]
        countryAM,

        [Description("Aruba")]
        countryAW,

        [Description("Australia")]
        countryAU,

        [Description("Austria")]
        countryAT,

        [Description("Azerbaijan")]
        countryAZ,

        [Description("Bahamas")]
        countryBS,

        [Description("Bahrain")]
        countryBH,

        [Description("Bangladesh")]
        countryBD,

        [Description("Barbados")]
        countryBB,

        [Description("Belarus")]
        countryBY,

        [Description("Belgium")]
        countryBE,

        [Description("Belize")]
        countryBZ,

        [Description("Benin")]
        countryBJ,

        [Description("Bermuda")]
        countryBM,

        [Description("Bhutan")]
        countryBT,

        [Description("Bolivia")]
        countryBO,

        [Description("Bosnia and Herzegovina")]
        countryBA,

        [Description("Botswana")]
        countryBW,

        [Description("Bouvet Island")]
        countryBV,

        [Description("Brazil")]
        countryBR,

        [Description("British Indian Ocean Territory")]
        countryIO,

        [Description("Brunei Darussalam")]
        countryBN,

        [Description("Bulgaria")]
        countryBG,

        [Description("Burkina Faso")]
        countryBF,

        [Description("Burundi")]
        countryBI,

        [Description("Cambodia")]
        countryKH,

        [Description("Cameroon")]
        countryCM,

        [Description("Canada")]
        countryCA,

        [Description("Cape Verde")]
        countryCV,

        [Description("Cayman Islands")]
        countryKY,

        [Description("Central African Republic")]
        countryCF,

        [Description("Chad")]
        countryTD,

        [Description("Chile")]
        countryCL,

        [Description("China")]
        countryCN,

        [Description("Christmas Island")]
        countryCX,

        [Description("Cocos (Keeling) Islands")]
        countryCC,

        [Description("Colombia")]
        countryCO,

        [Description("Comoros")]
        countryKM,

        [Description("Congo")]
        countryCG,

        [Description("Congo, the Democratic Republic of the")]
        countryCD,

        [Description("Cook Islands")]
        countryCK,

        [Description("Costa Rica")]
        countryCR,

        [Description("Cote D'ivoire")]
        countryCI,

        [Description("Croatia (Hrvatska)")]
        countryHR,

        [Description("Cuba")]
        countryCU,

        [Description("Cyprus")]
        countryCY,

        [Description("Czech Republic")]
        countryCZ,

        [Description("Denmark")]
        countryDK,

        [Description("Djibouti")]
        countryDJ,

        [Description("Dominica")]
        countryDM,

        [Description("Dominican Republic")]
        countryDO,

        [Description("East Timor")]
        countryTP,

        [Description("Ecuador")]
        countryEC,

        [Description("Egypt")]
        countryEG,

        [Description("El Salvador")]
        countrySV,

        [Description("Equatorial Guinea")]
        countryGQ,

        [Description("Eritrea")]
        countryER,

        [Description("Estonia")]
        countryEE,

        [Description("Ethiopia")]
        countryET,

        [Description("European Union")]
        countryEU,

        [Description("Falkland Islands (Malvinas)")]
        countryFK,

        [Description("Faroe Islands")]
        countryFO,

        [Description("Fiji")]
        countryFJ,

        [Description("Finland")]
        countryFI,

        [Description("France")]
        countryFR,

        [Description("France, Metropolitan")]
        countryFX,

        [Description("French Guiana")]
        countryGF,

        [Description("French Polynesia")]
        countryPF,

        [Description("French Southern Territories")]
        countryTF,

        [Description("Gabon")]
        countryGA,

        [Description("Gambia")]
        countryGM,

        [Description("Georgia")]
        countryGE,

        [Description("Germany")]
        countryDE,

        [Description("Ghana")]
        countryGH,

        [Description("Gibraltar")]
        countryGI,

        [Description("Greece")]
        countryGR,

        [Description("Greenland")]
        countryGL,

        [Description("Grenada")]
        countryGD,

        [Description("Guadeloupe")]
        countryGP,

        [Description("Guam")]
        countryGU,

        [Description("Guatemala")]
        countryGT,

        [Description("Guinea")]
        countryGN,

        [Description("Guinea-Bissau")]
        countryGW,

        [Description("Guyana")]
        countryGY,

        [Description("Haiti")]
        countryHT,

        [Description("Heard Island and Mcdonald Islands")]
        countryHM,

        [Description("Holy See (Vatican City State)")]
        countryVA,

        [Description("Honduras")]
        countryHN,

        [Description("Hong Kong")]
        countryHK,

        [Description("Hungary")]
        countryHU,

        [Description("Iceland")]
        countryIS,

        [Description("India")]
        countryIN,

        [Description("Indonesia")]
        countryID,

        [Description("Iran, Islamic Republic of")]
        countryIR,

        [Description("Iraq")]
        countryIQ,

        [Description("Ireland")]
        countryIE,

        [Description("Israel")]
        countryIL,

        [Description("Italy")]
        countryIT,

        [Description("Jamaica")]
        countryJM,

        [Description("Japan")]
        countryJP,

        [Description("Jordan")]
        countryJO,

        [Description("Kazakhstan")]
        countryKZ,

        [Description("Kenya")]
        countryKE,

        [Description("Kiribati")]
        countryKI,

        [Description("Korea, Democratic People's Republic of")]
        countryKP,

        [Description("Korea, Republic of")]
        countryKR,

        [Description("Kuwait")]
        countryKW,

        [Description("Kyrgyzstan")]
        countryKG,

        [Description("Lao People's Democratic Republic")]
        countryLA,

        [Description("Latvia")]
        countryLV,

        [Description("Lebanon")]
        countryLB,

        [Description("Lesotho")]
        countryLS,

        [Description("Liberia")]
        countryLR,

        [Description("Libyan Arab Jamahiriya")]
        countryLY,

        [Description("Liechtenstein")]
        countryLI,

        [Description("Lithuania")]
        countryLT,

        [Description("Luxembourg")]
        countryLU,

        [Description("Macao")]
        countryMO,

        [Description("Macedonia, the Former Yugosalv Republic of")]
        countryMK,

        [Description("Madagascar")]
        countryMG,

        [Description("Malawi")]
        countryMW,

        [Description("Malaysia")]
        countryMY,

        [Description("Maldives")]
        countryMV,

        [Description("Mali")]
        countryML,

        [Description("Malta")]
        countryMT,

        [Description("Marshall Islands")]
        countryMH,

        [Description("Martinique")]
        countryMQ,

        [Description("Mauritania")]
        countryMR,

        [Description("Mauritius")]
        countryMU,

        [Description("Mayotte")]
        countryYT,

        [Description("Mexico")]
        countryMX,

        [Description("Micronesia, Federated States of")]
        countryFM,

        [Description("Moldova, Republic of")]
        countryMD,

        [Description("Monaco")]
        countryMC,

        [Description("Mongolia")]
        countryMN,

        [Description("Montserrat")]
        countryMS,

        [Description("Morocco")]
        countryMA,

        [Description("Mozambique")]
        countryMZ,

        [Description("Myanmar")]
        countryMM,

        [Description("Namibia")]
        countryNA,

        [Description("Nauru")]
        countryNR,

        [Description("Nepal")]
        countryNP,

        [Description("Netherlands")]
        countryNL,

        [Description("Netherlands Antilles")]
        countryAN,

        [Description("New Caledonia")]
        countryNC,

        [Description("New Zealand")]
        countryNZ,

        [Description("Nicaragua")]
        countryNI,

        [Description("Niger")]
        countryNE,

        [Description("Nigeria")]
        countryNG,

        [Description("Niue")]
        countryNU,

        [Description("Norfolk Island")]
        countryNF,

        [Description("Northern Mariana Islands")]
        countryMP,

        [Description("Norway")]
        countryNO,

        [Description("Oman")]
        countryOM,

        [Description("Pakistan")]
        countryPK,

        [Description("Palau")]
        countryPW,

        [Description("Palestinian Territory")]
        countryPS,

        [Description("Panama")]
        countryPA,

        [Description("Papua New Guinea")]
        countryPG,

        [Description("Paraguay")]
        countryPY,

        [Description("Peru")]
        countryPE,

        [Description("Philippines")]
        countryPH,

        [Description("Pitcairn")]
        countryPN,

        [Description("Poland")]
        countryPL,

        [Description("Portugal")]
        countryPT,

        [Description("Puerto Rico")]
        countryPR,

        [Description("Qatar")]
        countryQA,

        [Description("Reunion")]
        countryRE,

        [Description("Romania")]
        countryRO,

        [Description("Russian Federation")]
        countryRU,

        [Description("Rwanda")]
        countryRW,

        [Description("Saint Helena")]
        countrySH,

        [Description("Saint Kitts and Nevis")]
        countryKN,

        [Description("Saint Lucia")]
        countryLC,

        [Description("Saint Pierre and Miquelon")]
        countryPM,

        [Description("Saint Vincent and the Grenadines")]
        countryVC,

        [Description("Samoa")]
        countryWS,

        [Description("San Marino")]
        countrySM,

        [Description("Sao Tome and Principe")]
        countryST,

        [Description("Saudi Arabia")]
        countrySA,

        [Description("Senegal")]
        countrySN,

        [Description("Serbia and Montenegro")]
        countryCS,

        [Description("Seychelles")]
        countrySC,

        [Description("Sierra Leone")]
        countrySL,

        [Description("Singapore")]
        countrySG,

        [Description("Slovakia")]
        countrySK,

        [Description("Slovenia")]
        countrySI,

        [Description("Solomon Islands")]
        countrySB,

        [Description("Somalia")]
        countrySO,

        [Description("South Africa")]
        countryZA,

        [Description("South Georgia and the South Sandwich Islands")]
        countryGS,

        [Description("Spain")]
        countryES,

        [Description("Sri Lanka")]
        countryLK,

        [Description("Sudan")]
        countrySD,

        [Description("Suriname")]
        countrySR,

        [Description("Svalbard and Jan Mayen")]
        countrySJ,

        [Description("Swaziland")]
        countrySZ,

        [Description("Sweden")]
        countrySE,

        [Description("Switzerland")]
        countryCH,

        [Description("Syrian Arab Republic")]
        countrySY,

        [Description("Taiwan, Province of China")]
        countryTW,

        [Description("Tajikistan")]
        countryTJ,

        [Description("Tanzania, United Republic of")]
        countryTZ,

        [Description("Thailand")]
        countryTH,

        [Description("Togo")]
        countryTG,

        [Description("Tokelau")]
        countryTK,

        [Description("Tonga")]
        countryTO,

        [Description("Trinidad and Tobago")]
        countryTT,

        [Description("Tunisia")]
        countryTN,

        [Description("Turkey")]
        countryTR,

        [Description("Turkmenistan")]
        countryTM,

        [Description("Turks and Caicos Islands")]
        countryTC,

        [Description("Tuvalu")]
        countryTV,

        [Description("Uganda")]
        countryUG,

        [Description("Ukraine")]
        countryUA,

        [Description("United Arab Emirates")]
        countryAE,

        [Description("United Kingdom")]
        countryUK,

        [Description("United States")]
        countryUS,

        [Description("United States Minor Outlying Islands")]
        countryUM,

        [Description("Uruguay")]
        countryUY,

        [Description("Uzbekistan")]
        countryUZ,

        [Description("Vanuatu")]
        countryVU,

        [Description("Venezuela")]
        countryVE,

        [Description("Vietnam")]
        countryVN,

        [Description("Virgin Islands, British")]
        countryVG,

        [Description("Virgin Islands, U.S.")]
        countryVI,

        [Description("Wallis and Futuna")]
        countryWF,

        [Description("Western Sahara")]
        countryEH,

        [Description("Yemen")]
        countryYE,

        [Description("Yugoslavia")]
        countryYU,

        [Description("Zambia")]
        countryZM,

        [Description("Zimbabwe")]
        CountryZW
    }
}
