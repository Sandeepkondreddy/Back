   M S B A M L   `   `   `  ����U  QUniversalReaderAssistant, Version=2.8.16.16, Culture=neutral, PublicKeyToken=null1clr-namespace:ThingMagic.URA2ThingMagic.URA2  5           ThingMagic.URA2.UserMemory   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:ThingMagic.URA2     5      .��5	      �� -   5        $��spUserMemory��5	   N     �� Orientation$  Vertical=�6   $��5q�6(   $
��Left=�63   {�.  $
��1080��6t   $	��Top=�6�   $	��613��6�    � 	Resources 5
   
      boolToVisible��%   &	        �� 5      6+   5      .+�5   
   �� -   5        $��tagUserMemory��5      
���������5      �� 5      $
��Bold"�6   Tag to Read/Write6A   .��5      � -   5        $��grid1��5   !   $
��auto��6   $
��auto��6.   $��5q�6;   .+�5      	� -   5        $��rbFirstTagUserMemTb��5   �   $��First tag to respond ��6D    	� 	GroupName$ 
UsermemTab��6d    d� 	IsChecked$
 TrueY�6{   $��16��6�   $
��Left=�6�   $��15,6,0,0q�6�   $	��Top=�6�   	� 5      -   5        $��rbSelectedTagUserMemTb��5   �    ^� ShowOnDisabled .  � ToolTip$: 4Go to Tag Results tab and right-click on a data cell��6C   $&�� Tag selected in Tag Results page��6�   $ 
UsermemTab��6�   $��16��6�   $
��Left=�6�   $��	15,28,0,0q�6�   $	��Top=�6  {�.  64  � 5      -   5        $��gdTagSelectionCriteria��5      $
��Left=�6�   $	��910��6�   y�65   ��  �� ElementName$ rbSelectedTag�� �� Path$ 	IsChecked� �� 	Converter# [  .+�5      �� $  Vertical=�6!   $��	22,46,2,4q�68   .+�5      �� -   5        $��lblSelectFilter��5       $
��Left=�67   $��Showing tag:��6S   $
��auto��6l   $	��Top=�6y   6�   �� 5      -   5        $��lblUserMemoryError��5   W   $��Error��6    $��23��60   $
��Left=�6<   $��Center=�6q   $y�	Collapsed=�6�   $
��auto��6�   ���	#FFF60909	 J� OpacityMask	 �	#FFFA00006�   5      �� 5      -	   5        $��txtEpc��5   =   $��23��6   $��	0,21,71,0q�6*   $	��Top=�6K   $y�Hidden=�6c   $��Right=�6w   $��28��6�   �� 5      -
   5        $��btnRead��5   g   $
��Read��6   $��23��6,   $
��Left=�68   $��
212,13,0,0q�6S   $	��Top=�6v   $��75��6�   6�   �� 5      -   5        $��txtData��5   Y   $��23��6   $
��Left=�6*   $��
647,21,0,0q�6E   $	��Top=�6h   $y�Hidden=�6�   $��28��6�   5      5      5      �� 5   
   -   5        $��gbUMRep��5      
���������5       �� 5!      $
��Bold"�6   #!How To Represent User Memory Data6R   .��5#      � -   5        $��gbRepresent��5#   +   $
��auto��6   $��5q�6    .+�5$      	� -   5        $��rbHexRep��5$   Q   $
��Left=�6   $	��Top=�69   $ RepresentUserMem��6a   $
 TrueY�6~   $��Addressed Hexadecimal��6�   $��10,0,0,0q�6�   6�   	� 5%      -   5        $��rbASCII��5%      $
��Left=�6-   $	��Top=�6H   $ RepresentUserMem��6`   $ FalseY�6}   $��ASCII Notepad��6�   $��	10,20,0,0q�6�   6�   5&      5'      �� 5(   
   $  Vertical=�6   .+�5)      �� -   5        $��gbHexRep��5)   8   $
��Auto��6   
�������y�6H   �� *	IsChecked+$ rbHexRep��# [  ��5*      �� 5+      $
��Bold"�6   Addressed Hex Editor6D   .��5-      � $
��Auto��6   .+�5.      �� -   5        $��btnWrite��5.      {�.  $��Write To Tag��6?   $��15,0,0,0q�6V   $	��100��6h   $
��Left=�6t   $��25��6�   $	��Top=�6�   6�   �� 5/      $��WrapWithOverflow=�6    
 �� TextAlignment$

 Left=�6@   $��Byte Address (Hex)��6U   $��30��6o   $��68��6{   $��	20,40,0,0q�6�   $	��Top=�6�   $
��Bold"�6�   $
��Left=�6�   $��10(�6�   � 50      .+�51      �� $  
Horizontal=�6%   $��0,35,0,0q�6>   $��Right=�6P   $	��278��6l   .+�52      �� -   5        $%��txtblkHexByteSp64BytesAvailable��52   9   
���  ��$
��Left=�6`   $	��Top=�6{   $��
Space for ��6�   6�   �� 53      -   5        $.��(txtblkSpMAXSizeHexByteSp64BytesAvailable��53   9   
���  ��$
��Left=�6i   $	��Top=�6�   $��00��6�   6�   �� 54      -   5        $"��txtblkSpHexByteSp64Bytes_End��54   9   
���  ��$
��Left=�6]   $	��Top=�6x   $�� bytes available��6�   6�   55      � 56      -   5        $��grdpnlByteAddress��56      $	��215��68   $
��Left=�6E   $��
20,60,0,95q�6`   $
��1030��6t   .+�57      �� -   5        $��stkpnlHexClmnAddress��57   <   $��	10,10,0,0q�6)   $	��Top=�6X   $
��Left=�6p   $  
Horizontal=�6�   $
��Auto��6�   6�   �� 58      -   5        $ ��hexAddressEdtrScrollViewer��58   +   $
��Auto��6M   $
��1025��6[   $
��Auto=�6h   $
��Auto=�6�   $
��Left=�6�   $��
0,34,0,-34q�6�   .��59   "   �� -   5        $��stkpnlByteAddress��59   .   $	��Top=�6G   $
��Left=�6_   $  Vertical=�6z   $
��Auto��6�   5:      5;      5<      5=      �� 5>      -   5        $��lblErrorAddHexEdtr��5>      $�� ��66   
���  ��$��30��6R   $
��Auto��6^   $��
98,324,0,0q�6k   $	��Top=�6   $
��Left=�6�   5?      5@      �� 5A      -   5        $��
gbAsciiRep��5A   *   
�������y�6<   �� *	IsChecked+$ rbASCII��# [  ��5B      �� 5C      $
��Bold"�6   ASCII Text Editor6A   .��5E      � $
��auto��6   .+�5F      �� -   5        $��btnWriteAscii��5F      {�.  $��Write To Tag��6D   $��15,0,0,0q�6[   $	��100��6m   $
��Left=�6y   $��26��6�   $	��Top=�6�   6�   �� 5G      $  
Horizontal=�6!   $��
590,40,0,0q�6:   .+�5H      �� -   5        $��txtblkSp64CharsAvailable��5H   5   
���  ��$
��Left=�6U   $	��Top=�6p   $��
Space for ��6�   6�   �� 5I      -   5        $#��txtblkSpMAXSizeCharsAvailable��5I   5   
���  ��$
��Left=�6Z   $	��Top=�6u   $��00��6�   6�   �� 5J      -   5        $"��txtblkSp64CharsAvailable_End��5J   5   
���  ��$
��Left=�6Y   $	��Top=�6t   $�� characters available: ��6�   6�   �� 5K      -   5        $(��"txtblkRemainingCharsAvailableCount��5K   $   
���  ��$��00 characters remaining��6_   $
��Left=�6~   $	��Top=�6�   6�   5L      �� 5M      -    5        $��txtASCIIData��5M     {�.   �� 
CaretBrush
 �   �$��Consolas)�6�   ���	#0A000000 � Cursor$ Arrow��6�   $	��250��6�   $
��Left=�6�   $��	15,32,0,0q�6  $	��900��6.   �� TextWrapping$
 Wrap=�6;  " �� VerticalScrollBarVisibility$
 Auto=�6O   �� 	MaxLength$ 0��6r   �� AcceptsReturn .  �� 
AcceptsTab .  �� IsUndoEnabled .  .��6�   J� CommandBindings 5N      )  #System.Windows.Input.CommandBinding  5O      -!   5           Command$ Paste��5O   -   6`     5P      -"   5        $	 Cut��5P   -   6`   .��5Q   3   5R      �� 5S      -#   5        $��lblErrorAddAsciiEdtr��5S      $�� ��68   
���  ��$��30��6T   $
��Auto��6`   $��
18,324,0,0q�6m   $	��Top=�6�   $
��Left=�6�   5T      5U      5V      5W      5X      