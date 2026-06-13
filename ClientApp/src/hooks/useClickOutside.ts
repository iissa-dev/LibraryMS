import { useEffect, useRef, type RefObject } from "react";

/**
 * Custom hook that detects mouse clicks outside of a specified DOM element.
 * Useful for closing dropdowns, modals, popovers, or responsive menus.
 *
 * @param {() => void} callback - The function to execute when a click outside occurs.
 * @returns {RefObject<HTMLDivElement | null>} A React mutable ref object to attach to the target container element.
 * * @example
 * const modalRef = useClickOutside(() => setIsOpen(false));
 * return <div ref={modalRef}>Modal Content</div>;
 */
const useClickOutside = (
  callback: () => void,
): RefObject<HTMLDivElement | null> => {
  const elementRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    const handleClickOutside = (e: MouseEvent) => {
      if (
        elementRef.current &&
        !elementRef.current.contains(e.target as Node)
      ) {
        callback();
      }
    };

    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, [callback]);

  return elementRef;
};

export default useClickOutside;
