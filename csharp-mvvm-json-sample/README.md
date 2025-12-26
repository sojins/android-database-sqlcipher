# WPF MVVM JSON 예제 (.NET Framework 4.8 / C# 8)

간단한 노트 편집기를 통해 JSON 파일을 읽고/저장하는 MVVM 샘플입니다. 제목은 최대 20자, 본문은 300자로 제한되며 본문 미리보기(300자)를 제공합니다.

## 프로젝트 구성

```
csharp-mvvm-json-sample/
├── README.md
└── WpfMvvmJsonSample/
    ├── App.xaml
    ├── App.xaml.cs
    ├── Commands/
    │   └── RelayCommand.cs
    ├── MainWindow.xaml
    ├── MainWindow.xaml.cs
    ├── Models/
    │   └── Note.cs
    ├── Services/
    │   └── JsonNoteService.cs
    ├── ViewModels/
    │   └── NoteViewModel.cs
    └── WpfMvvmJsonSample.csproj
```

## 주요 포인트

- **JsonNoteService**: `INoteStorageService` 구현체로 JSON 파일을 비동기 입출력합니다.
- **NoteViewModel**: 제목 20자, 본문 300자를 강제하며 저장/불러오기/새로 작성 명령을 제공합니다.
- **MainWindow.xaml**: 제목/본문 입력, 미리보기, 저장/불러오기 버튼 UI.
- **패키지**: `Newtonsoft.Json 13.0.3` 사용.

## 실행 방법

1. Visual Studio에서 `WpfMvvmJsonSample.csproj`를 엽니다.
2. NuGet 복원 후 실행합니다. 실행 시 `note.json`을 실행 디렉터리에 생성/저장합니다.
