   M S B A M L   `   `   `  ����  UniversalReaderAssistant#    ThingMagic.URA2.ucUntraceable   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      �� -   5        $��spUntraceable��5   N     �� Orientation$  Vertical=�6   $��5q�6(   $
��Left=�63   {�.  $	��580��6u   $	��Top=�6�   $
��Auto��6�    � 	Resources 5	   
      boolToVisible��%   &	        �� 5
      6+   5      .+�5   
   �� -   5        $��tagUntraceable��5      
���������5      �� 5      $
��Bold"�6   Tag to Read/Write6A   .��5      � -   5        $��grid1��5   !   $
��auto��6   $
��auto��6.   $��5q�6;   .+�5      	� -   5        $��rbFirstTagUntraceableTb��5   �   $��First tag to respond ��6H    	� 	GroupName$ UntraceableTab��6h    d� 	IsChecked$
 TrueY�6�   $��16��6�   $
��Left=�6�   $��15,6,0,0q�6�   $	��Top=�6�   	� 5      -   5        $ ��rbSelectedTagUntraceableTb��5      ^� ShowOnDisabled .  � ToolTip$: 4Go to Tag Results tab and right-click on a data cell��6C   $&�� Tag selected in Tag Results page��6�   $ UntraceableTab��6�   $��16��6�   $
��Left=�6�   $��	15,28,0,0q�6�   $	��Top=�6%  {�.  6=  � 5      -   5        $��gdTagSelectionCriteria��5      $
��Left=�65   .+�5      �� $  Vertical=�6!   $��	22,46,2,4q�68   .+�5      �� -   5        $��lblSelectFilter��5       $
��Left=�67   $��Showing tag:��6S   $
��auto��6l   $	��Top=�6y   6�   �� 5      -   5        $��lblUntraceableTagError��5   W   $��Error��6    $��23��60   $
��Left=�6<   $��Center=�6u   $y�	Collapsed=�6�   $
��auto��6�   ���	#FFF60909 J� OpacityMask �	#FFFA00006�   5      5      �� 5      -	   5        $��txtEpc��5   =   $��23��6   $��	0,21,71,0q�6*   $	��Top=�6K   $y�Hidden=�6c   $��Right=�6w   $��28��6�   �� 5      -
   5        $��btnRead��5   g   $
��Read��6   $��23��6,   $
��Left=�68   $��
212,13,0,0q�6S   $	��Top=�6v   $��75��6�   6�   �� 5      -   5        $��txtData��5   Y   $��23��6   $
��Left=�6*   $��
647,21,0,0q�6E   $	��Top=�6h   $y�Hidden=�6�   $��28��6�   5      5      �� 5   
   -   5        $��spUntraceableFields��5      $
��Auto��60   $
��Auto��6=   
�������$��	0,10,0,20q�6^   .+�5      �� $  
Horizontal=�6   $��10q�62   .+�5       �� -   5        $��lblAccesspwd��5    2   $��Access Password��6   $
��Auto��6F   $
��Auto��6T   $��0,5,5,5q�6a   �� 5!      -   5        $��txtbxAccesspaasword��5!   �    �� 
CaretBrush
 �   ����	#0A000000$�� ��6E   $��80��6�   $��10,0,0,0q�6�   $��23��6�    �� 	MaxLength$ 8��6�   5"      �� 5$      -   5        $	��Epc��5$      $��0q�6"   $
��Auto��66   ��5%      �� 5&      $
��Bold"�6    EPC67   .��5(      � -   5        $��grdEpc��5(   %   $
��Auto��6   $
��Auto��63   $��5q�6@   .+�5)      	� -   5        $��rdBtnShowEntireEpc��5)   <   $��Show Entire EPC��6"   $ 
epcUntrace��6V   $��0q�6m   $
 TrueY�6x   6�   �� 5*      $  
Horizontal=�6!   $
��Auto��6:   $
��Auto��6H   $��0,21,0,0q�6U   .+�5+      	� -   5        $��rdBtnShowSpecificEpc��5+   &   $��Show EPC to length=��6C   $��0,4,0,0q�6a   $ 
epcUntrace��6r   $��4,0,2,0q�6�   6�   �� 5,      -   5        $��txtbxEpcLen��5,   "   {�.  $��17��6G   $��25��6S   $��0q�6^   $ 2��6i   6�   �� 5-      $��Words��6    $
��Auto��60   5.      5/      50      �� 52      -   5        $	��TID��52      $��0q�6"   $
��Auto��66   ��53      �� 54      $
��Bold"�6    TID67   .��56      � -   5        $��grdTid��56   %   $
��Auto��6   $
��Auto��63   $��5q�6@   .+�57      	� -   5        $��rdbtnShowAllTid��57   "   $��Show All��69   $ 
tidUntrace��6L   $��0q�6c   $
 TrueY�6n   6   	� 58      -   5        $��rdbtnShowTaginfo��58   "   $��Show Tag Info Only��6:   $ 
tidUntrace��6W   $��0,21,0,0q�6n   6�   	� 59      -   5        $��rdbtnShowNone��59   "   $��	Show None��67   $ 
tidUntrace��6K   $��0,42,0,0q�6b   6t   5:      5;      �� 5=      -   5        $
��User��5=      $��0q�6#   $
��Auto��67   ��5>      �� 5?      $
��Bold"�6    USER68   .��5A      � -   5        $��grdUser��5A   %   $
��Auto��6   $
��Auto��64   $��5q�6A   .+�5B      	� -   5        $��rdbtnShowUserMem��5B   "   $��Show All��6:   $ userUntrace��6M   $��0q�6e   $
 TrueY�6p   6�   	� 5C      -   5        $��rdbtnHideUserMem��5C   "   $��	Show None��6:   $ userUntrace��6N   $��0,21,0,0q�6f   6x   5D      5E      �� 5G      -   5        $��btmWriteToTag��5G   B   $��Write to Tag��6   $��250,0,250,10q�6,   $��1q�6W   6�   5H      5I      5J      