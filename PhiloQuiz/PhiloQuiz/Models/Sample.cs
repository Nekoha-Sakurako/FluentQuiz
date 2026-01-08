using System.Collections.Generic;

namespace PhiloQuiz
{
    public static class Comp0
    {
        public static (IEnumerable<SingleChoiceQuestion> SingleChoice, IEnumerable<TrueFalseQuestion> TrueFalse) GetDefault()
        {
            var single = new List<SingleChoiceQuestion>
            {
// 计算机网络技术与应用（1-25题）
new SingleChoiceQuestion(1, "世界上第一个计算机网络是（ ）",
    new[]{"A. Internet", "B. Ethernet", "C. ARPANET", "D. NSFNET"}, "C"),
new SingleChoiceQuestion(2, "计算机网络最突出的优点是（ ）",
    new[]{"A. 运算速度快", "B. 存储容量大", "C. 资源共享", "D. 精度高"}, "C"),
new SingleChoiceQuestion(3, "下列不属于计算机网络功能的是（ ）",
    new[]{"A. 资源共享", "B. 数据传输", "C. 分布式处理", "D. 病毒防护"}, "D"),
new SingleChoiceQuestion(4, "按地理范围划分，校园网属于（ ）",
    new[]{"A. 局域网", "B. 城域网", "C. 广域网", "D. 互联网"}, "A"),
new SingleChoiceQuestion(5, "在总线型拓扑结构中，所有节点共享一条（ ）",
    new[]{"A. 环型信道", "B. 广播信道", "C. 点对点信道", "D. 专用信道"}, "B"),
new SingleChoiceQuestion(6, "星型拓扑结构的中心节点是（ ）",
    new[]{"A. 计算机", "B. 交换机", "C. 路由器", "D. 集线器"}, "B"),
new SingleChoiceQuestion(7, "下列传输介质中，抗干扰能力最强的是（ ）",
    new[]{"A. 双绞线", "B. 同轴电缆", "C. 光纤", "D. 无线电波"}, "C"),
new SingleChoiceQuestion(8, "下列属于无线传输介质的是（ ）",
    new[]{"A. 双绞线", "B. 同轴电缆", "C. 光纤", "D. 微波"}, "D"),
new SingleChoiceQuestion(9, "在数据通信系统中，实现数字信号转换为模拟信号的设备是（ ）",
    new[]{"A. 编码器", "B. 解码器", "C. 调制解调器", "D. 放大器"}, "C"),
new SingleChoiceQuestion(10, "数据传输速率的单位是（ ）",
    new[]{"A. 字节/秒", "B. 比特/秒", "C. 帧/秒", "D. 分组/秒"}, "B"),
new SingleChoiceQuestion(11, "下列多路复用技术中，将信道按时间分割成多个时隙的是（ ）",
    new[]{"A. 频分多路复用", "B. 时分多路复用", "C. 波分多路复用", "D. 码分多路复用"}, "B"),
new SingleChoiceQuestion(12, "异步传输的特点是（ ）",
    new[]{"A. 以数据块为单位传输", "B. 需要严格的时钟同步", "C. 每个字符独立传输", "D. 传输效率高"}, "C"),
new SingleChoiceQuestion(13, "下列数字编码方式中，每个比特中间都有跳变的是（ ）",
    new[]{"A. 不归零码", "B. 曼彻斯特编码", "C. 差分曼彻斯特编码", "D. 双极性编码"}, "B"),
new SingleChoiceQuestion(14, "在交换技术中，需要建立专用通路的是（ ）",
    new[]{"A. 电路交换", "B. 报文交换", "C. 分组交换", "D. 信元交换"}, "A"),
new SingleChoiceQuestion(15, "光纤通信利用的原理是（ ）",
    new[]{"A. 电信号传输", "B. 无线电波传输", "C. 光信号的全反射", "D. 声波传输"}, "C"),
new SingleChoiceQuestion(16, "双绞线中，UTP代表（ ）",
    new[]{"A. 无屏蔽双绞线", "B. 屏蔽双绞线", "C. 超五类双绞线", "D. 六类双绞线"}, "A"),
new SingleChoiceQuestion(17, "信号在传输过程中，幅度逐渐减弱的现象称为（ ）",
    new[]{"A. 延迟", "B. 衰减", "C. 失真", "D. 干扰"}, "B"),
new SingleChoiceQuestion(18, "误码率是指（ ）",
    new[]{"A. 传输错误的比特数/传输总比特数", "B. 传输正确的比特数/传输总比特数", "C. 传输错误的分组数/传输总分组数", "D. 传输正确的分组数/传输总分组数"}, "A"),
new SingleChoiceQuestion(19, "OSI参考模型共有（ ）",
    new[]{"A. 5层", "B. 6层", "C. 7层", "D. 8层"}, "C"),
new SingleChoiceQuestion(20, "在OSI参考模型中，负责比特流在物理介质上传输的是（ ）",
    new[]{"A. 物理层", "B. 数据链路层", "C. 网络层", "D. 传输层"}, "A"),
new SingleChoiceQuestion(21, "数据链路层的协议数据单元是（ ）",
    new[]{"A. 比特", "B. 帧", "C. 分组", "D. 报文"}, "B"),
new SingleChoiceQuestion(22, "在OSI参考模型中，实现路由选择功能的是（ ）",
    new[]{"A. 物理层", "B. 数据链路层", "C. 网络层", "D. 传输层"}, "C"),
new SingleChoiceQuestion(23, "TCP/IP参考模型共有（ ）",
    new[]{"A. 3层", "B. 4层", "C. 5层", "D. 7层"}, "B"),
new SingleChoiceQuestion(24, "在TCP/IP参考模型中，IP协议位于（ ）",
    new[]{"A. 网络接口层", "B. 网际层", "C. 传输层", "D. 应用层"}, "B"),
new SingleChoiceQuestion(25, "下列协议中，属于传输层协议的是（ ）",
    new[]{"A. IP", "B. TCP", "C. HTTP", "D. FTP"}, "B"),

// 计算机组装与维护（26-45题）
new SingleChoiceQuestion(26, "以使用的逻辑元件为依据，下面关于计算机发展历程叙述正确的是（ ）",
    new[]{"A. 晶体管计算机、中小规模集成电路计算机、电子管计算机、大规模集成电路计算机", "B. 电子管计算机、晶体管计算机、中小规模集成电路计算机、大规模集成电路计算机", "C. 晶体管计算机、电子管计算机、中小规模集成电路计算机、大规模集成电路计算机", "D. 电子管计算机、中小规模集成电路计算机、晶体管计算机、大规模集成电路计算机"}, "B"),
new SingleChoiceQuestion(27, "目前主流的内存类型为（ ）",
    new[]{"A. EDO", "B. SDRAM", "C. DDR", "D. RDRAM"}, "C"),
new SingleChoiceQuestion(28, "下列关于CPU核心的发展方向不正确的是（ ）",
    new[]{"A. 高电压", "B. 低功耗", "C. 多核心", "D. 高频率"}, "A"),
new SingleChoiceQuestion(29, "下列哪种接口用于Intel CPU？（ ）",
    new[]{"A. Socket AMM3", "B. Socket FM2+", "C. LGA", "D. SlotA"}, "C"),
new SingleChoiceQuestion(30, "集成显卡的显存通过共享（ ）实现",
    new[]{"A. 内存", "B. 外存", "C. 主板缓存", "D. CPU缓存"}, "A"),
new SingleChoiceQuestion(31, "按照主板结构的紧凑程度划分，下列主板面积由大到小排序正确的是（ ）",
    new[]{"A. ATX、Micro-ATX、ITX", "B. ITX、ATX、Micro-ATX", "C. ATX、ITX、Micro-ATX", "D. Micro-ATX、ATX、ITX"}, "A"),
new SingleChoiceQuestion(32, "下列（ ）不与南桥芯片相连",
    new[]{"A. 内存", "B. 键盘", "C. 网卡", "D. 硬盘"}, "A"),
new SingleChoiceQuestion(33, "Type-C接口属于的接口规范是（ ）",
    new[]{"A. USB 1.1", "B. USB 2.0", "C. USB 3.0", "D. USB 3.1"}, "D"),
new SingleChoiceQuestion(34, "主板上的南桥芯片主要负责连接（ ）",
    new[]{"A. CPU", "B. 显卡", "C. 内存", "D. 外设"}, "D"),
new SingleChoiceQuestion(35, "下列关于动态随机存储器的叙述正确的是（ ）",
    new[]{"A. 相对于静态随机存储器读取速率快", "B. 适合用作CPU的内置缓存", "C. 主要用作计算机的主存储器", "D. 结构复杂、成本高、功耗大"}, "C"),
new SingleChoiceQuestion(36, "SATA3.0标准的理论最高数据传输率为（ ）",
    new[]{"A. 1Gbit/s", "B. 2Gbit/s", "C. 3Gbits", "D. 6Gbit/s"}, "D"),
new SingleChoiceQuestion(37, "相对于固态硬盘，关于机械硬盘的特点，下面叙述错误的是（ ）",
    new[]{"A. 容量大", "B. 速度快", "C. 抗震性能差", "D. 性价比高"}, "B"),
new SingleChoiceQuestion(38, "下列（ ）不是机械硬盘的主要性能指标",
    new[]{"A. 转速", "B. 寻道时间", "C. 单碟容量", "D. 尺寸大小"}, "D"),
new SingleChoiceQuestion(39, "DDR4内存的工作电压为（ ）",
    new[]{"A. 1.2V", "B. 1.5V", "C. 2.4V", "D. 3V"}, "A"),
new SingleChoiceQuestion(40, "固态硬盘采用的存储介质为（ ）",
    new[]{"A. 磁盘", "B. 磁芯", "C. 闪存颗粒", "D. 磁带"}, "C"),
new SingleChoiceQuestion(41, "下列不支持热插拔的硬盘接口类型是（ ）",
    new[]{"A. IDE", "B. SATA", "C. SCSI", "D. USB"}, "A"),
new SingleChoiceQuestion(42, "下列关于硬盘性能指标的叙述不正确的是（ ）",
    new[]{"A. 硬盘的转速越快，外部数据的传输率越高", "B. 提高单碟容量可以缩短寻道时间和等待时间，降低硬盘成本", "C. 平均访问时间包括硬盘的寻道时间和等待时间", "D. 硬盘的高速缓存越大，性能越好"}, "A"),
new SingleChoiceQuestion(43, "下列关于固态硬盘的叙述不正确的是（ ）",
    new[]{"A. 固态硬盘采用半导体存储技术", "B. 相同接口的固态硬盘的读写速率与机械硬盘的读写速率相同", "C. 相同容量下，固态硬盘价格高于机械硬盘价格", "D. 固态硬盘中没有机械结构"}, "B"),
new SingleChoiceQuestion(44, "显示芯片是显卡的核心，称为（ ）",
    new[]{"A. GPU", "B. RAMDAC", "C. PGU", "D. UPS"}, "A"),
new SingleChoiceQuestion(45, "下列（ ）是电源的主要性能指标",
    new[]{"A. 价格", "B. 功率", "C. 体积", "D. 重量"}, "B")
            };

            var tf = new List<TrueFalseQuestion>
            {
// 计算机组装与维护（46-55题）
new TrueFalseQuestion(1, "在选购主板时，主板芯片组要与选择的CPU类型相匹配", true),
new TrueFalseQuestion(2, "CPU散热器表面积越大，散热效果越好", false),
new TrueFalseQuestion(3, "DRAM在计算机系统中通常用于CPU内部缓存", false),
new TrueFalseQuestion(4, "SATA接口采用并行传输方式，不支持热插拔", false),
new TrueFalseQuestion(5, "固态硬盘比机械硬盘的抗震性好", true),
new TrueFalseQuestion(6, "PCI-EX16标准专为显卡设计", false),
new TrueFalseQuestion(7, "显卡的流处理单元个数越多，处理能力越强", true),
new TrueFalseQuestion(8, "液晶显示器的主要成本来自背光模组", false),
new TrueFalseQuestion(9, "LCD不存在刷新频率的概念", false),
new TrueFalseQuestion(10, "板载声卡比独立声卡的音质输出效果好", true),
            };

            return (single, tf);
        }
    }

    // 移出到命名空间级别，确保在项目其他文件中直接使用 SingleChoiceQuestion / TrueFalseQuestion
    public record SingleChoiceQuestion10(int Id, string Question, string[] Options, string Answer);
    public record TrueFalseQuestion10(int Id, string Question, bool Answer);
}