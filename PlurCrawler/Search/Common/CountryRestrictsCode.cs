using PlurCrawler.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Search.Common
{
    public enum CountryRestrictsCode
    {
        [Note("제한 없음")]
        All,

        [Note("Afghanistan")]
        countryAF,

        [Note("Albania")]
        countryAL,

        [Note("Algeria")]
        countryDZ,

        [Note("American Samoa")]
        countryAS,

        [Note("Andorra")]
        countryAD,

        [Note("Angola")]
        countryAO,

        [Note("Anguilla")]
        countryAI,

        [Note("Antarctica")]
        countryAQ,

        [Note("Antigua and Barbuda")]
        countryAG,

        [Note("Argentina")]
        countryAR,

        [Note("Armenia")]
        countryAM,

        [Note("Aruba")]
        countryAW,

        [Note("Australia")]
        countryAU,

        [Note("Austria")]
        countryAT,

        [Note("Azerbaijan")]
        countryAZ,

        [Note("Bahamas")]
        countryBS,

        [Note("Bahrain")]
        countryBH,

        [Note("Bangladesh")]
        countryBD,

        [Note("Barbados")]
        countryBB,

        [Note("Belarus")]
        countryBY,

        [Note("Belgium")]
        countryBE,

        [Note("Belize")]
        countryBZ,

        [Note("Benin")]
        countryBJ,

        [Note("Bermuda")]
        countryBM,

        [Note("Bhutan")]
        countryBT,

        [Note("Bolivia")]
        countryBO,

        [Note("Bosnia and Herzegovina")]
        countryBA,

        [Note("Botswana")]
        countryBW,

        [Note("Bouvet Island")]
        countryBV,

        [Note("Brazil")]
        countryBR,

        [Note("British Indian Ocean Territory")]
        countryIO,

        [Note("Brunei Darussalam")]
        countryBN,

        [Note("Bulgaria")]
        countryBG,

        [Note("Burkina Faso")]
        countryBF,

        [Note("Burundi")]
        countryBI,

        [Note("Cambodia")]
        countryKH,

        [Note("Cameroon")]
        countryCM,

        [Note("Canada")]
        countryCA,

        [Note("Cape Verde")]
        countryCV,

        [Note("Cayman Islands")]
        countryKY,

        [Note("Central African Republic")]
        countryCF,

        [Note("Chad")]
        countryTD,

        [Note("Chile")]
        countryCL,

        [Note("China")]
        countryCN,

        [Note("Christmas Island")]
        countryCX,

        [Note("Cocos (Keeling) Islands")]
        countryCC,

        [Note("Colombia")]
        countryCO,

        [Note("Comoros")]
        countryKM,

        [Note("Congo")]
        countryCG,

        [Note("Congo, the Democratic Republic of the")]
        countryCD,

        [Note("Cook Islands")]
        countryCK,

        [Note("Costa Rica")]
        countryCR,

        [Note("Cote D'ivoire")]
        countryCI,

        [Note("Croatia (Hrvatska)")]
        countryHR,

        [Note("Cuba")]
        countryCU,

        [Note("Cyprus")]
        countryCY,

        [Note("Czech Republic")]
        countryCZ,

        [Note("Denmark")]
        countryDK,

        [Note("Djibouti")]
        countryDJ,

        [Note("Dominica")]
        countryDM,

        [Note("Dominican Republic")]
        countryDO,

        [Note("East Timor")]
        countryTP,

        [Note("Ecuador")]
        countryEC,

        [Note("Egypt")]
        countryEG,

        [Note("El Salvador")]
        countrySV,

        [Note("Equatorial Guinea")]
        countryGQ,

        [Note("Eritrea")]
        countryER,

        [Note("Estonia")]
        countryEE,

        [Note("Ethiopia")]
        countryET,

        [Note("European Union")]
        countryEU,

        [Note("Falkland Islands (Malvinas)")]
        countryFK,

        [Note("Faroe Islands")]
        countryFO,

        [Note("Fiji")]
        countryFJ,

        [Note("Finland")]
        countryFI,

        [Note("France")]
        countryFR,

        [Note("France, Metropolitan")]
        countryFX,

        [Note("French Guiana")]
        countryGF,

        [Note("French Polynesia")]
        countryPF,

        [Note("French Southern Territories")]
        countryTF,

        [Note("Gabon")]
        countryGA,

        [Note("Gambia")]
        countryGM,

        [Note("Georgia")]
        countryGE,

        [Note("Germany")]
        countryDE,

        [Note("Ghana")]
        countryGH,

        [Note("Gibraltar")]
        countryGI,

        [Note("Greece")]
        countryGR,

        [Note("Greenland")]
        countryGL,

        [Note("Grenada")]
        countryGD,

        [Note("Guadeloupe")]
        countryGP,

        [Note("Guam")]
        countryGU,

        [Note("Guatemala")]
        countryGT,

        [Note("Guinea")]
        countryGN,

        [Note("Guinea-Bissau")]
        countryGW,

        [Note("Guyana")]
        countryGY,

        [Note("Haiti")]
        countryHT,

        [Note("Heard Island and Mcdonald Islands")]
        countryHM,

        [Note("Holy See (Vatican City State)")]
        countryVA,

        [Note("Honduras")]
        countryHN,

        [Note("Hong Kong")]
        countryHK,

        [Note("Hungary")]
        countryHU,

        [Note("Iceland")]
        countryIS,

        [Note("India")]
        countryIN,

        [Note("Indonesia")]
        countryID,

        [Note("Iran, Islamic Republic of")]
        countryIR,

        [Note("Iraq")]
        countryIQ,

        [Note("Ireland")]
        countryIE,

        [Note("Israel")]
        countryIL,

        [Note("Italy")]
        countryIT,

        [Note("Jamaica")]
        countryJM,

        [Note("Japan")]
        countryJP,

        [Note("Jordan")]
        countryJO,

        [Note("Kazakhstan")]
        countryKZ,

        [Note("Kenya")]
        countryKE,

        [Note("Kiribati")]
        countryKI,

        [Note("Korea, Democratic People's Republic of")]
        countryKP,

        [Note("Korea, Republic of")]
        countryKR,

        [Note("Kuwait")]
        countryKW,

        [Note("Kyrgyzstan")]
        countryKG,

        [Note("Lao People's Democratic Republic")]
        countryLA,

        [Note("Latvia")]
        countryLV,

        [Note("Lebanon")]
        countryLB,

        [Note("Lesotho")]
        countryLS,

        [Note("Liberia")]
        countryLR,

        [Note("Libyan Arab Jamahiriya")]
        countryLY,

        [Note("Liechtenstein")]
        countryLI,

        [Note("Lithuania")]
        countryLT,

        [Note("Luxembourg")]
        countryLU,

        [Note("Macao")]
        countryMO,

        [Note("Macedonia, the Former Yugosalv Republic of")]
        countryMK,

        [Note("Madagascar")]
        countryMG,

        [Note("Malawi")]
        countryMW,

        [Note("Malaysia")]
        countryMY,

        [Note("Maldives")]
        countryMV,

        [Note("Mali")]
        countryML,

        [Note("Malta")]
        countryMT,

        [Note("Marshall Islands")]
        countryMH,

        [Note("Martinique")]
        countryMQ,

        [Note("Mauritania")]
        countryMR,

        [Note("Mauritius")]
        countryMU,

        [Note("Mayotte")]
        countryYT,

        [Note("Mexico")]
        countryMX,

        [Note("Micronesia, Federated States of")]
        countryFM,

        [Note("Moldova, Republic of")]
        countryMD,

        [Note("Monaco")]
        countryMC,

        [Note("Mongolia")]
        countryMN,

        [Note("Montserrat")]
        countryMS,

        [Note("Morocco")]
        countryMA,

        [Note("Mozambique")]
        countryMZ,

        [Note("Myanmar")]
        countryMM,

        [Note("Namibia")]
        countryNA,

        [Note("Nauru")]
        countryNR,

        [Note("Nepal")]
        countryNP,

        [Note("Netherlands")]
        countryNL,

        [Note("Netherlands Antilles")]
        countryAN,

        [Note("New Caledonia")]
        countryNC,

        [Note("New Zealand")]
        countryNZ,

        [Note("Nicaragua")]
        countryNI,

        [Note("Niger")]
        countryNE,

        [Note("Nigeria")]
        countryNG,

        [Note("Niue")]
        countryNU,

        [Note("Norfolk Island")]
        countryNF,

        [Note("Northern Mariana Islands")]
        countryMP,

        [Note("Norway")]
        countryNO,

        [Note("Oman")]
        countryOM,

        [Note("Pakistan")]
        countryPK,

        [Note("Palau")]
        countryPW,

        [Note("Palestinian Territory")]
        countryPS,

        [Note("Panama")]
        countryPA,

        [Note("Papua New Guinea")]
        countryPG,

        [Note("Paraguay")]
        countryPY,

        [Note("Peru")]
        countryPE,

        [Note("Philippines")]
        countryPH,

        [Note("Pitcairn")]
        countryPN,

        [Note("Poland")]
        countryPL,

        [Note("Portugal")]
        countryPT,

        [Note("Puerto Rico")]
        countryPR,

        [Note("Qatar")]
        countryQA,

        [Note("Reunion")]
        countryRE,

        [Note("Romania")]
        countryRO,

        [Note("Russian Federation")]
        countryRU,

        [Note("Rwanda")]
        countryRW,

        [Note("Saint Helena")]
        countrySH,

        [Note("Saint Kitts and Nevis")]
        countryKN,

        [Note("Saint Lucia")]
        countryLC,

        [Note("Saint Pierre and Miquelon")]
        countryPM,

        [Note("Saint Vincent and the Grenadines")]
        countryVC,

        [Note("Samoa")]
        countryWS,

        [Note("San Marino")]
        countrySM,

        [Note("Sao Tome and Principe")]
        countryST,

        [Note("Saudi Arabia")]
        countrySA,

        [Note("Senegal")]
        countrySN,

        [Note("Serbia and Montenegro")]
        countryCS,

        [Note("Seychelles")]
        countrySC,

        [Note("Sierra Leone")]
        countrySL,

        [Note("Singapore")]
        countrySG,

        [Note("Slovakia")]
        countrySK,

        [Note("Slovenia")]
        countrySI,

        [Note("Solomon Islands")]
        countrySB,

        [Note("Somalia")]
        countrySO,

        [Note("South Africa")]
        countryZA,

        [Note("South Georgia and the South Sandwich Islands")]
        countryGS,

        [Note("Spain")]
        countryES,

        [Note("Sri Lanka")]
        countryLK,

        [Note("Sudan")]
        countrySD,

        [Note("Suriname")]
        countrySR,

        [Note("Svalbard and Jan Mayen")]
        countrySJ,

        [Note("Swaziland")]
        countrySZ,

        [Note("Sweden")]
        countrySE,

        [Note("Switzerland")]
        countryCH,

        [Note("Syrian Arab Republic")]
        countrySY,

        [Note("Taiwan, Province of China")]
        countryTW,

        [Note("Tajikistan")]
        countryTJ,

        [Note("Tanzania, United Republic of")]
        countryTZ,

        [Note("Thailand")]
        countryTH,

        [Note("Togo")]
        countryTG,

        [Note("Tokelau")]
        countryTK,

        [Note("Tonga")]
        countryTO,

        [Note("Trinidad and Tobago")]
        countryTT,

        [Note("Tunisia")]
        countryTN,

        [Note("Turkey")]
        countryTR,

        [Note("Turkmenistan")]
        countryTM,

        [Note("Turks and Caicos Islands")]
        countryTC,

        [Note("Tuvalu")]
        countryTV,

        [Note("Uganda")]
        countryUG,

        [Note("Ukraine")]
        countryUA,

        [Note("United Arab Emirates")]
        countryAE,

        [Note("United Kingdom")]
        countryUK,

        [Note("United States")]
        countryUS,

        [Note("United States Minor Outlying Islands")]
        countryUM,

        [Note("Uruguay")]
        countryUY,

        [Note("Uzbekistan")]
        countryUZ,

        [Note("Vanuatu")]
        countryVU,

        [Note("Venezuela")]
        countryVE,

        [Note("Vietnam")]
        countryVN,

        [Note("Virgin Islands, British")]
        countryVG,

        [Note("Virgin Islands, U.S.")]
        countryVI,

        [Note("Wallis and Futuna")]
        countryWF,

        [Note("Western Sahara")]
        countryEH,

        [Note("Yemen")]
        countryYE,

        [Note("Yugoslavia")]
        countryYU,

        [Note("Zambia")]
        countryZM,

        [Note("Zimbabwe")]
        CountryZW
    }
}
