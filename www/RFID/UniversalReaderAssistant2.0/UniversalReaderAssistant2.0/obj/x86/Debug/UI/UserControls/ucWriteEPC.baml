   M S B A M L   `   `   `  ����  UniversalReaderAssistant     ThingMagic.URA2.ucWriteEPC   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      �� -   5        $��
spWriteEPC��5   O     �� Orientation$  Vertical=�6   $��10q�6(   $
��Left=�64   $	��600��6a   {�.  $	��Top=�6   .+�5	   
   �� -   5        $��
tagtowrite��5	      
���������5
      �� 5      $
��Bold"�6   
Write to68   .��5      � -   5        $��grid1��5   !   $
��auto��6   $
��auto��6.   $��5q�6;    � 	Resources 5         boolToVisible��%   &	        �� 5      63   5      .+�5      	� -   5        $��
rbFirstTag��5   �   $��First tag to respond ��6;    	� 	GroupName$ Tagtowriteto��6[    d� 	IsChecked$
 TrueY�6t   $��16��6�   $
��Left=�6�   $��15,6,0,0q�6�   $	��Top=�6�   	� 5      -   5        $��rbSelectedTag��5      ^� ShowOnDisabled .  � ToolTip$: 4Go to Tag Results tab and right-click on a data cell��6C   $&�� Tag selected in Tag Results page��6�   $ Tagtowriteto��6�   $��16��6�   $
��Left=�6�   $��	15,28,0,0q�6�   $	��Top=�6  {�.  6.  � 5      -   5        $��gdTagSelectionCriteria��5      y�65   ��  �� ElementName$ rbSelectedTag�� �� Path$ 	IsChecked� �� 	Converter# [  .+�5      �� $  Vertical=�6!   $��	22,46,2,4q�68   .+�5      �� -   5        $��lblFilterDisplay��5       $
��Left=�68   $!��Select Filter for WriteEPC:��6T   $
��auto��6|   $	��Top=�6�   6�   �� 5      -   5        $��lblSelectFilter��5   H   $�� ��6    $
��Left=�6,   $	��Top=�6_   $
��auto��6w   5      �� 5      -	   5        $��txtEpc��5   Y   $��23��6   $
��Left=�6*   $��
220,20,0,0q�6E   $	��Top=�6g   $��28��6   $y�Hidden=�6�   �� 5      -
   5        $��txtData��5   Y   $��23��6   $
��Left=�6*   $��
270,20,0,0q�6E   $	��Top=�6h   $y�Hidden=�6�   $��28��6�   5      5      5      �� 5   
   -   5        $��gbRespresentEPC��5      
���������5      �� 5      $
��Bold"�6   How to represent EPC6D   .��5!   
   � -   5        $��grid2��5!      $��75��6   $
��auto��6(   .+�5"      	� -   5        $��rbHexRep��5"   �   $��Hexadecimal��6   $��16��64   $ RepresentEPC��6[   $
 TrueY�6t   $
��Left=�6�   $��18,6,0,0q�6�   $	��Top=�6�   $	��101��6�   	� 5#      -   5        $��
rbASCIIRep��5#   �   $��
ASCII Text��6   $��16��6/   $ RepresentEPC��6X   $
��Left=�6q   $��	18,28,0,0q�6�   $	��Top=�6�   	� 5$      -   5        $��rbReverseBase36Rep��5$   �   $��Reverse Base 36��6   $��16��64   $ RepresentEPC��6e   $
��Left=�6~   $��	18,50,0,0q�6�   $	��Top=�6�   5%      5&      �� 5'   
   -   5        $��gbCurrentEPC��5'      
���������5(      �� 5)      $
��Bold"�6   Current EPC6;   .��5+      � -   5        $��grid3��5+      $��80��6   $
��auto��6,   $��2q�69   .+�5,      �� -   5        $��txtCurrentEpc��5,   I   $��22��6   	 � Cursor$	 Arrow��6&   $��
16,16,0,42q�65   ���	#0A000000$��Center=�6v   $
��Left=�6�   $	��283��6�   
 �� CharacterCasing$
 Normal=�6�   $��Consolas)�6�    �� TextWrapping$
 Wrap=�6�    �� 
IsReadOnly .  " �� VerticalScrollBarVisibility$
 Auto=�6  .��6�   J� CommandBindings 5-      )  #System.Windows.Input.CommandBinding  5.      -   5           Command$ Paste��5.   )   69     5/      -   5        $	 Cut��5/   )   67   .��50   /    � ContextMenu 51      �� 52      $y�	Collapsed=�6&   .��53   +   54      �� 55      -   5        $��btnRead��55   d   $
��Read��6   $��23��6(   $
��Left=�64   $��313,16,0,42q�6O   $��Center=�6s   $
��auto��6�   6�   �� 56      -   5        $��lblError��56   c   $��Error��6   $��23��6(   $
��Left=�64   $��
18,43,0,15q�6O   $��Center=�6s   $y�	Collapsed=�6�   $
��auto��6�   ���	#FFF60909 J� OpacityMask �	#FFFA00006�   57      58      �� 59   
   -   5        $��gbNewEPC��59      
���������5:      �� 5;      $
��Bold"�6   	New EPC67   .��5=   
   � -   5        $��grid4��5=      $��50��6   $
��auto��6(   .+�5>      �� -   5        $��txtWriteEPC��5>   a   $��23��6   $��
20,11,0,16q�6&    �� 
CaretBrush
 �   �$
 Wrap=�6M   ���	#0A000000$��Center=�6�   $
 Normal=�6�    �� MaxLines$ 62��6�   $��Consolas)�6�   $
��Left=�6�   $	��283��6�   $
 Auto=�6
  �� 5?      -   5        $��btnWrite��5?   d   $��Write��6   $��23��6)   $
��Left=�65   $��
315,9,0,18q�6P   $��Center=�6t   $
��auto��6�   6�   5@      5A      5B      5C      