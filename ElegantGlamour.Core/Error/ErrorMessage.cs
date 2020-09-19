﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElegantGlamour.Core.Error
{
    public static class ErrorMessage
    {
        public static string Err_Category_Title_Not_Empty => "Le titre de la catégorie ne peut pas être vide";
        public static string Err_Category_Title_Max_Size => "La taille maximale du titre est de 50 caractères";
        public static string Err_Category_Not_Empty => "La catégorie ne peut pas être à vide";
        public static string Err_Category_Already_Exist => "La catégorie existe dejà";
        public static string Err_Category_Does_Not_Exist => "La catégorie n'existe pas";
        public static string Err_Prestation_Title_Not_Empty => "Le titre de la préstation ne peut pas être vide";
        public static string Err_Prestation_Description_Not_Empty => "La description de la préstation ne peut pas être vide";
        public static string Err_Prestation_Price_Not_Empty => "Le prix de la préstation ne peut pas être vide";
        public static string Err_Prestation_Duration_Not_Empty => "La durée de la préstation ne peut pas être vide";
        public static string Err_Prestation_Duration_Not_Equal_To_0 => "La durée de la préstation ne peut pas être équale à 0";


    }
}
