import dayjs from "dayjs";

const Footer = () => {
  const currentYear = dayjs();
  return (
    <div className="mt-4 flex items-center justify-between mb-4 md:mb-0 flex-col md:flex-row">
      <div className="text-xs font-mono text-neutral/60 justify-center gap-1.5">
        &copy; {currentYear.year()} Lexicon Systems. All Rights Reserved.
      </div>
      <div className="text-xs font-mono text-neutral/60 justify-center gap-1.5">
        Crafted with{"  "}
        {<span className="text-red animate-pulse text-base">♥</span>} by{" "}
        <a
          href="https://iissa.dev"
          target="_blank"
          className="text-text-secondary hover:text-primary transition-colors"
        >
          IIssadev
        </a>
      </div>
    </div>
  );
};

export default Footer;
