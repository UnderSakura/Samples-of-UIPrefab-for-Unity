# Samples-of-UIPrefab-for-Unity
## [更新]一些关于列表滑动、悬浮卡牌等模板显示在Unity.UI中
About UI Prefabs of Digital_Exhibition.

> 目前已更新列表滑动的UIPrefab的制作
* 列表滑动 (已完成) `正在完善`
* 悬浮卡牌
* 放映机效果

### 列表滑动的制作思路
使用`ScrollView`在`ScriptableObject`的数据中动态加载相关内容，并添加到`ScrollRect.content`中，以达到可滑动列表的效果。
> 计划设计可反转的滑片，利用`Raycast`检测对应正反面的触发器，控制`Animator`来进行滑片反转的动效。
