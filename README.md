 Как устроен код 
Система разделена на “Core/Makeup” и “GameEvents” с DI через Zenject:
• GameEventsDispatcher — простая шина событий. GameEventsController в Update выполняет очередь DispatchOnUpdate. См. GameEventsDispatcher.cs, GameEventsController.cs.
• MakeupFlowService — центральный координатор шагов макияжа: хранит текущий стиль и делегирует обработку стратегиям. Подписывается на MakeupEndEvent. См. MakeupFlowService.cs.
• Стратегии (CreamMakeupStrategy, EyesShadowsMakeupStrategy, LipstickMakeupStrategy) описывают последовательность действий руки и применения макияжа. См. AbstractMakeupStrategy.cs, CreamMakeupStrategy.cs, EyesShadowsMakeupStrategy.cs, LipstickMakeupStrategy.cs.
• MakeupStepResolver собирает “runtime шаг” из статических настроек (MakeupStepStaticSettings) и ссылок сцены (MakeupWindowReferences). См. MakeupStepResolver.cs, MakeupStepStaticSettings.cs, MakeupWindowReferences.cs.
• UI-рука разделена на HandView (ввод/drag) и HandAnimator (анимации), объединены фасадом MakeupHandView. См. HandView.cs, HandAnimator.cs, MakeupHandView.cs.
• Применение результата — через FaceMakeupRenderer, который управляет MakeupItemAnimator (fade). См. FaceMakeupRenderer.cs, MakeupItemAnimator.cs.
• Ввод пользователя для выбора стиля — MakeupTapHandler (PointerDown → событие). См. MakeupTapHandler.cs.
Основные технические моменты реализации
1. Используется DI (Zenject): GameInstaller связывает настройки/виды/сервисы, GameEventsInstaller — событийную шину. См. GameInstaller.cs, GameEventsInstaller.cs.
2. Архитектура “стратегий” позволяет разную последовательность действий для разных типов макияжа, но общий поток задаёт MakeupFlowService.
3. Данные шага собираются из двух источников: ScriptableObject (статические параметры, например ResultAlpha) и ссылки на элементы сцены (RectTransform позиций, палитры, applicator).
4. Визуальная часть UI основана на RectTransform и анимациях через DOTween (Move/Fade/Jump/MakeupTween).
5. Состояние шага хранится в MakeupStepRuntime — это структура данных, которую передают стратегии.
6. Ввод сделан через HandView (Update + Input API), а не через UI drag‑события.
