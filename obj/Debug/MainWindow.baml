   M S B A M L   `   `   `  ����I  EWhack-a-Button, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Whack_a_ButtonWhack_a_Button  5   	       Whack_a_Button.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:Whack_a_Button     5   	     � Title$  Whack-a-Button��5   	   $	��600��6    $	��750��6-   
������� � 	Resources 5
      ,   &Whack_a_Button.BoolVisibilityConverter   BoolVisibilityConverter��%   &	          5   
   6(   5      .��5      � $��Center=�6   $��Center=�6(   
���   �$	��500��6V   $	��650��6c   .+�5   
   �� $��Whack-a-Button��6   
���   �
�������$��Calibri)�6P   $��36(�6e    �� TextAlignment$ Center=�6s   $��	0,100,0,0q�6�   y�6�   �� *startVisible+ �� 	Converter# [   �� Mode$ TwoWay=��� 5   
   $��Timer:��6   
���   �
�������$��Calibri)�6H   $��16(�6]   $ Center=�6k   $��359,403,0,0q�6�   y�6�   �� *startVisible+# [  $ TwoWay=�6�   �� 5   
   
������� J� Opacity$	 0.8Q�6W   $��Center=�6e   $��Center=�6�   $��25��6�   $��40��6�   $��533,385,72,61q�6'  ��6   �� *time+$ TwoWay=�y�6�   �� *startVisible+# [  $ TwoWay=�6'  �� 5   
   $��75��6   $	��200��6   $��Start��6)   
�����F�$
 0.75Q�6P   $��Center=�6�   $��0,200,0,195q�6  y�6_   �� *startVisible+# [  $ TwoWay=���6�   �� *StartCommand+6  �� 5   
   -   5        $��ClickButton��5      $��25��6$   $��25��60   $	 0.8Q�6y   ��6;   �� *buttonColour+��6_   �� *margin+y�6�   �� *buttonVisible+# [  ��6�   �� *ButtonCommand+�� 5   
   $��25��6   $��25��6   
���  ��$	 0.8Q�6X   ��69   �� *otherMargin+y�6f   �� *buttonVisible+# [  ��6�   �� *FakeCommand+�� 5   
   $��
TIME'S UP!��6   
���   �
�������$��Calibri)�6L   $��24(�6a   $ Center=�6o   $��	0,156,0,0q�6�   y�6�   �� *exitVisible+# [  $ TwoWay=��� 5   
   
�������$��0,198,0,169q�6�   $��Calibri)�6�   $��24(�6�   $ Center=�6�   ��6   �� *points+y�6>   �� *exitVisible+# [  $ TwoWay=�6�   �� 5   
   
�������$��	0,238,0,0q�6�   $ Center=�6�   ��6   �� *	finalTime+y�6A   �� *exitVisible+# [  $ TwoWay=�6�   �� 5   
   
�������$��	0,268,0,0q�6�   $ Center=�6�   ��6   �� *pointsPerSec+y�6D   �� *exitVisible+# [  $ TwoWay=�6�   �� 5   
   -   5        $
��Exit��5      $
��Exit��6   
�������$	 0.8Q�6>   $��50��6L   $	��200��6X   $��329,314,113,80q�6�   y�6d   �� *exitVisible+# [  $ TwoWay=���6�   �� *ExitCommand+6�   �� 5   
   -   5        $��Retry��5      $��Retry��6   
������$
 0.75Q�6F   $��50��6U   $	��200��6a   $��113,314,329,80q�6�   y�6m   �� *exitVisible+# [  $ TwoWay=���6�   �� *RetryCommand+6�   5      5      