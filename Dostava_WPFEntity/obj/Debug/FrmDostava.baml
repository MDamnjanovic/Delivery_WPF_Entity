   M S B A M L   `   `   `  ����L  HDostava_WPFEntity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Dostava_WPFEntityDostava_WPFEntity  5   	   "    Dostava_WPFEntity.FrmDostava   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Dostava_WPFEntity     5   	     � Title$  
FrmDostava��5   	   $��553.151��6   $��1149.427��6-   .��5	      � .+�5
   
   �� $��Adresa dostave��6   $
��Left=�6)   $��31��6D   $��
722,48,0,0q�6P   $	��Top=�6d   $	��180��6|   �� 5   
   $��Datum��6   $
��Left=�6    $��31��6;   $��722,109,0,0q�6G   $	��Top=�6\   $	��180��6t   �� 5   
   -   5         ��Name$ tbAdresa��5   1   $
��Left=�6C   $��31��6^   $��
835,48,0,0q�6j    �� TextWrapping$
 Wrap=�6~   $	��Top=�6�   $	��220��6�   ��6   �� *adresaDostave+6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ dtDatum��5   ,   $
��Left=�6=   $��31��6X   $��835,109,0,0q�6d   $	��Top=�6y   $	��220��6�      Text 6   �� *datum+6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 	dgArtikli��5      $
��Left=�6&   $	��311��6A   $��	54,48,0,0q�6N   $	��Top=�6a   $	��623��6y      
IsReadOnly .    SelectionMode$ Single=�6�   �� 5   
   -   5         ��Name$ btnPotvrdiPorudzbinu��5      $��Potvrdi porudzbinu��6/   $
��Left=�6L   $��27��6g   $��749,475,0,0q�6s   $	��Top=�6�   $	��153��6�   6�   �� 5   
   -   5        $ btnDodajArtikal��5      $��Dodaj artikal��6*   $
��Left=�6B   $��37��6]   $��469,386,0,0q�6i   $	��Top=�6~   $	��208��6�   6�   �� 5   
   -   5        	 ��Name$	 lbArtikliPorudzbina��5      $
��Left=�6/   $	��235��6J   $��734,188,0,0q�6W   $	��Top=�6l   $	��321��6�   .@�5      �� $��Label��6   5      �� 5   
   $��Kolicina��6   $
��Left=�6#   $��27��6>   $��
57,396,0,0q�6J   $	��Top=�6^   $	��171��6v   �� 5   
   -   5        $ 
tbKolicina��5      $
��Left=�6&   $��27��6A   $��190,396,0,0q�6M   $
 Wrap=�6b   $	��Top=�6v   $	��147��6�   6�   5      5      