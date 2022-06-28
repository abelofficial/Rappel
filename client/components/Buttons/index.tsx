import Image from "next/image";
import React, { MouseEventHandler } from "react";

export interface ButtonProps {
  iconHeight?: number;
  iconWidth?: number;
  onSubmitHandler?: MouseEventHandler<HTMLImageElement>;
}

export const AddRoundedIconButton = (props: ButtonProps) => {
  return (
    <div onClick={props.onSubmitHandler} style={{ padding: "0.3rem 0.3rem" }}>
      <Image
        src='/add-icon.svg'
        alt='An SVG of an eye'
        width={props.iconWidth ? props.iconWidth : 22}
        height={props.iconHeight ? props.iconHeight : 22}
      />
    </div>
  );
};

export const EditIconButton = (props: ButtonProps) => {
  return (
    <div onClick={props.onSubmitHandler} style={{ padding: "0.3rem 0.3rem" }}>
      <Image
        src='/edit-icon.svg'
        alt='An SVG of an eye'
        width={props.iconWidth ? props.iconWidth : 22}
        height={props.iconHeight ? props.iconHeight : 22}
      />
    </div>
  );
};

export const SettingIconButton = (props: ButtonProps) => {
  return (
    <div onClick={props.onSubmitHandler} style={{ padding: "0.3rem 0.3rem" }}>
      <Image
        src='/settings-icon.svg'
        alt='An SVG of an eye'
        width={props.iconWidth ? props.iconWidth : 22}
        height={props.iconHeight ? props.iconHeight : 22}
      />
    </div>
  );
};

export const StartIconButton = (props: ButtonProps) => {
  return (
    <div onClick={props.onSubmitHandler} style={{ padding: "0.3rem 0.3rem" }}>
      <Image
        src='/play-icon.svg'
        alt='An SVG of an eye'
        width={props.iconWidth ? props.iconWidth : 22}
        height={props.iconHeight ? props.iconHeight : 22}
      />
    </div>
  );
};

export const OffIconButton = (props: ButtonProps) => {
  return (
    <div onClick={props.onSubmitHandler} style={{ padding: "0.3rem 0.3rem" }}>
      <Image
        src='/turnoff-icon.svg'
        alt='An SVG of an eye'
        width={props.iconWidth ? props.iconWidth : 22}
        height={props.iconHeight ? props.iconHeight : 22}
      />
    </div>
  );
};

export const RestartIconButton = (props: ButtonProps) => {
  return (
    <div onClick={props.onSubmitHandler} style={{ padding: "0.3rem 0.3rem" }}>
      <Image
        src='/restart-icon.svg'
        alt='An SVG of an eye'
        width={props.iconWidth ? props.iconWidth : 22}
        height={props.iconHeight ? props.iconHeight : 22}
      />
    </div>
  );
};

export const BackIconButton = (props: ButtonProps) => {
  return (
    <div onClick={props.onSubmitHandler} style={{ padding: "0.3rem 0.3rem" }}>
      <Image
        src='/back-icon.svg'
        alt='An SVG of an eye'
        width={props.iconWidth ? props.iconWidth : 22}
        height={props.iconHeight ? props.iconHeight : 22}
      />
    </div>
  );
};
